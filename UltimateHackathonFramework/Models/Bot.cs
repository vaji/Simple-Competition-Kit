using System;
using System.Collections.Generic;
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

        public void RunBot()
        {
            throw new NotImplementedException();
        }

        public void KillBot()
        {
            throw new NotImplementedException();
        }
    }
}
