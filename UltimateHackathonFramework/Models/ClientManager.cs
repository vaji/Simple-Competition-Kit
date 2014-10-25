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
        private IList<IBot> _clients=new List<IBot>();

        public IList<IBot> Clients
        {
            get { return _clients; }
            set { _clients = value; }
        }

        public void ScanForClients()
        {
            string currentDirName = System.IO.Directory.GetCurrentDirectory();
            if(System.IO.Directory.Exists(currentDirName+@"\Bots"))
            {
                string[] subFolders = System.IO.Directory.GetDirectories(currentDirName + @"\Bots");
                foreach(string subFolder in subFolders)
                {
                    string [] partPath = subFolder.Split('\\');
                    string botName = partPath[partPath.Length - 1];
                    string path=subFolder+@"\Main.exe";
                    if(System.IO.File.Exists(path))
                    {
                        this.Clients.Add(new Bot(botName, path));
                    }
                }
            }
        }
    }
}
