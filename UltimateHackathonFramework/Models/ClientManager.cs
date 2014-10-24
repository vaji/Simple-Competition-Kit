using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    public class ClientManager : IClientManager
    {
        private IList<IBot> clients;

        public IList<IBot> Clients
        {
            get { return clients; }
            set { clients = value; }
        }

        public void ScanForClients()
        {
            string currentDirName = System.IO.Directory.GetCurrentDirectory();
            //foreach (System.IO.DirectoryInfo d in dirInfos)
            //{
             //   Console.WriteLine(d.Name);
            //}
        }
    }
}
