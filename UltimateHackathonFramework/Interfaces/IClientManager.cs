using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Interfaces
{
    interface IClientManager
    {
        private IList<IBot> clients;

        public IList<IBot> Clients
        {
            get { return clients; }
            set { clients = value; }
        }
        
        public void ScanForClients();
        
    }
}
