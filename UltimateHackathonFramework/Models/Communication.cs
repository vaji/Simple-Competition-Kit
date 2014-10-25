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

        Enums.ServerStateEnum _ServerState = Enums.ServerStateEnum.NotListening;

        private string _IpAddress;
        private int _PortNumber;


        public void StartListening(string IpAddress, int PortNumber)
        {
            _IpAddress = IpAddress;
            _PortNumber = PortNumber;
            try
            {
                listener = new TcpListener(IPAddress.Parse(IpAddress), PortNumber);
                listener.Start();
                _ServerState = Enums.ServerStateEnum.Listening;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void StopListening()
        {
            listener.Stop();
            _ServerState = Enums.ServerStateEnum.NotListening;
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


        public Enums.ServerStateEnum ServerState
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
