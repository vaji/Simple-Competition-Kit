using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Interfaces
{
    public interface ICommunication
    {
        void Connect(string IpAddress, int PortNumber);
        void Disconnect();

        bool IsConnected
        {
            get;
            set;
        }

        Dictionary<string, string> Send(Dictionary<string, string> message);

    }
}
