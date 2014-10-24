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
            if(System.IO.Directory.Exists(currentDirName+@"\Bots"))
            {
                string[] subFolders = System.IO.Directory.GetDirectories(currentDirName + @"\Bots");
                foreach(string subFolder in subFolders)
                {
                    //string
                    //string path=botName+@"\Main.exe";
                    //if(System.IO.File.Exists(path))
                    //{
                    //    this.Clients.Add(new Bot(botName, path));
                    //}
                }
            }
        }
    }
}
