using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    public class Bot : IBot
    {
        private string _id;
        private string _name;
        private string _path;
        private Process _process=null;
        private string _currentStatus;
        private TcpClient _CommunicationChannel = null;

        public string CurrentSatus
        {
            get { return _currentStatus; }
            set { _currentStatus = value; }
        }
        


        public Bot() { }
        public Bot(string name, string path)
        {
            this._id = name;
            this._name = name;
            this._path = path;
        }
        public string ID
        {
            get { return this._id; }
        }

        public string Name
        {
            get { return this._name; }
        }

        public Dictionary<string, string> Communicate(Dictionary<string, string> data)
        {
            string json = new JavaScriptSerializer().Serialize(data.ToDictionary(item => item.Key.ToString(), item => item.Value.ToString()));
            SendString(json);
            json = ReceiveString();
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        }

        private string ReceiveString()
        {
            NetworkStream stream = _CommunicationChannel.GetStream();
            byte[] buff = null;
            int buffSize = 4096;
            string result = "";

            while (!stream.DataAvailable) { };

            buff = new byte[buffSize];
            int length = stream.Read(buff, 0, buff.Length);
            if (length > 0 )
            {
                ASCIIEncoding asen = new ASCIIEncoding();
                result = asen.GetString(buff);
            }
            return result;
        }

        public void SendString(string message)
        {
            message = message + Environment.NewLine;
            if (_CommunicationChannel.Connected)
            {
                try
                {
                    ASCIIEncoding asen = new ASCIIEncoding();
                    byte[] bytes = asen.GetBytes(message);
                    NetworkStream stream = _CommunicationChannel.GetStream();
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
                catch (Exception e)
                {

                }
            }
            else
            {
                throw new Exception("Connection has not bee estabilished!");
            }
        }

        public void RunBot(ICommunication server)
        {
            if(File.Exists(_path))
            {
                _process = Process.Start(_path, server.IP + " " + server.Port);
            }
            else
            {
                throw new FileNotFoundException(_path);
            }
        }

        public void KillBot()
        {
            if(_process!=null)
            {
                _process.Kill();
                _process = null;
            }
            else
            {
                throw new Exception("Process has not been started yet. Invoke RunBot(...) first!");
            }
        }


        public System.Net.Sockets.TcpClient CommunicationChannel
        {
            get
            {
                return _CommunicationChannel;
            }
            set
            {
                _CommunicationChannel = value;
            }
        }


        public string CurrentStatus
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
