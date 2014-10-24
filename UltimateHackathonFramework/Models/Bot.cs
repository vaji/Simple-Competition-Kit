using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    public class Bot : IBot
    {
        private string id;
        private string name;
        private string path;
        private Process process=null;

        public Bot() { }
        public Bot(string name, string path)
        {
            this.id = name;
            this.name = name;
            this.path = path;
        }
        public string ID
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public Dictionary<string, string> Communicate(Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }

        public void RunBot(ICommunication server)
        {
            if(File.Exists(path))
            {
                process = Process.Start(path, server.IP + " " + server.Port);
            }
            else
            {
                throw new FileNotFoundException(path);
            }
        }

        public void KillBot()
        {
            throw new NotImplementedException();
        }
    }
}
