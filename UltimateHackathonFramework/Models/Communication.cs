using FirebirdSql.Data.FirebirdClient;
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

        Enums.ServerStateEnum _ServerState;

        private string _IpAddress;
        private int _PortNumber;

        public Communication()
        {
           ServerState = Enums.ServerStateEnum.NotListening;
        }
        public void StartListening(string IpAddress, int PortNumber)
        {
            _IpAddress = IpAddress;
            _PortNumber = PortNumber;
            try
            {
                listener = new TcpListener(IPAddress.Parse(IpAddress), PortNumber);
                listener.Start();
                ServerState = Enums.ServerStateEnum.Listening;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void StopListening()
        {
            listener.Stop();
            ServerState = Enums.ServerStateEnum.NotListening;
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
                return _ServerState;
            }
            set
            {
                if (_ServerState == value) return;
                _ServerState = value;
                OnServerStateChanged();
            }
        }

        private void OnServerStateChanged()
        {
            if (StateChanged != null)
                StateChanged();
        }


        public event Action StateChanged;
    }
}
