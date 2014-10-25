using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShipsBot
{
    public class Communication
    {
        TcpClient client = null;
        NetworkStream stream = null;
        Thread thread = null;

        bool isWorking = false;
        public bool connected = false;
        int buffSize = 1024;
        byte[] buff = null;

        public delegate void ServerWrite(string message);
        public event ServerWrite ServerWriteEvent;

        public delegate void ServerException(Exception exception);
        public event ServerException ServerExceptionEvent;

        public Communication()
        {

        }

        public void Connect(string ip, int port)
        {
            try
            {
                client = new TcpClient();
                client.Connect(ip, port);
                stream = client.GetStream();
                isWorking = true;
                connected = true;
                thread = new Thread(new ThreadStart(recieveLoop));
                thread.Start();
            }
            catch (Exception e)
            {
                if (ServerExceptionEvent != null)
                    ServerExceptionEvent(e);
                connected = false;
            }
        }

        void recieveLoop()
        {
            while (isWorking)
            {
                if (stream.DataAvailable)
                {
                    buff = new byte[buffSize];
                    int length = stream.Read(buff, 0, buff.Length);
                    if (length > 0 && ServerWriteEvent != null)
                    {
                        ASCIIEncoding asen = new ASCIIEncoding();
                        string s = asen.GetString(buff);

                        ServerWriteEvent(s);
                    }
                }
            }
        }

        public void SendString(string message)
        {
            message = message + Environment.NewLine;
            if (client.Connected)
            {
                try
                {
                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] bytes = asen.GetBytes(message);
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
                catch (Exception e)
                {
                    if (ServerExceptionEvent != null)
                        ServerExceptionEvent(e);
                    connected = false;
                }
            }
        }

        public void disconnect()
        {
            isWorking = false;

            if (thread != null)
            {
                while (thread.IsAlive)
                {

                }
            }
            if (client != null && client.Connected)
            {
                stream.Close();
                client.Close();
            }
            connected = false;
        }

        public void Dispose()
        {
            disconnect();
        }

    }
}
