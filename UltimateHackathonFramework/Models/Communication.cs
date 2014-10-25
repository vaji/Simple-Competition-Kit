using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{

    public class Communication : ICommunication
    {
        TcpListener listener=null;

        private string _IpAddress;
        private int _PortNumber;

        public static enum ServerStateEnum
        {
            Listening, NotListening
        }

        public void StartListening(string IpAddress, int PortNumber)
        {
            _IpAddress = IpAddress;
            _PortNumber = PortNumber;
            listener = new TcpListener(IPAddress.Parse(IpAddress), PortNumber);
            listener.Start();
        }

        public void StopListening()
        {
            listener.Stop();
        }


        public System.Net.Sockets.TcpClient GetConnectedClient()
        {
            return listener.AcceptTcpClient();
        }

        public string IP
        {
            get
            {
                return _IpAddress;
            }
            set
            {
                _IpAddress = value;
            }
        }

        public string Port
        {
            get
            {
                return _PortNumber.ToString();
            }
            set
            {
                _PortNumber = Convert.ToInt32(value);
            }
        }
    }
}
