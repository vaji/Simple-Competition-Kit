using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    public class Communication : ICommunication
    {



        public void Connect(string IpAddress, int PortNumber)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public bool IsConnected
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

        public Dictionary<string, string> Send(Dictionary<string, string> message)
        {
            throw new NotImplementedException();
        }

        public void StartListening(string IpAddress, int PortNumber)
        {
            throw new NotImplementedException();
        }

        public void StopListening()
        {
            throw new NotImplementedException();
        }
    }
}
