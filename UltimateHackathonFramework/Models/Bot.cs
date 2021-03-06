﻿using Newtonsoft.Json;
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
        private State _currentStatus;
        private TcpClient _CommunicationChannel = null;
        private ICommunication _server;
        private long _timeout = 1000;
        private string _directory;
        public State CurrentSatus
        {
            get { return _currentStatus; }
            set { _currentStatus = value; }
        }
        
        public Bot() { }
        public Bot(ICommunication server, string name, string path)
        {
            this._id = name;
            this._name = name;
            this._path = path;
            this._server = server;
            this._points = 0;
            this._directory = Path.GetDirectoryName(path);
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
            Trace.WriteLine(json);
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            catch (Exception)
            {
                return new Dictionary<string,string>(){{"error", "timeout"}};
            }
        }

        private string ReceiveString()
        {
            NetworkStream stream = _CommunicationChannel.GetStream();
            byte[] buff = null;
            int buffSize = 4096;
            string result = "";
            System.Diagnostics.Stopwatch timer=new System.Diagnostics.Stopwatch();
            timer.Start();
            while ((!stream.DataAvailable)&&(timer.ElapsedMilliseconds<=_timeout)) { };
            timer.Stop();

            buff = new byte[buffSize];
            int length = stream.Read(buff, 0, buff.Length);
            if (length > 0 )
            {
                ASCIIEncoding asen = new ASCIIEncoding();
                result = asen.GetString(buff);
            }
            else
            {
                return null; 
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
                    throw e;
                }
            }
            else
            {
                throw new Exception("Connection has not bee estabilished!");
            }
        }

        public void RunBot()
        {
            if(File.Exists(_path))
            {
                var processInfo = new ProcessStartInfo(_path, _server.IP + " " + _server.Port) { WindowStyle = ProcessWindowStyle.Minimized };
                _process = Process.Start(processInfo);
            }
            else
            {
                throw new FileNotFoundException(_path);
            }

            CommunicationChannel = _server.GetConnectedClient();
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

        private double _points;

        public double Points
        {
            get { return _points; }
            private set { _points = value; }
        }

        public void AddPoints(double points)
        {
            _points += points;
        }



        public State CurrentState
        {
            get
            {
                return _currentStatus;
            }
            set
            {
                _currentStatus=value;
            }
        }


        public void ClearPoints()
        {
            Points = 0;
        }


        public string Directory
        {
            get { return _directory; }
        }
    }
}
