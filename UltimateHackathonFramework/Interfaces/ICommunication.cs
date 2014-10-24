using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Interfaces
{
    public interface ICommunication
    {
        void StartListening(string IpAddress, int PortNumber);
        System.Net.Sockets.TcpClient GetConnectedClient();
        void StopListening();

        bool IsConnected
        {
            get;
            set;
        }
    }
}
