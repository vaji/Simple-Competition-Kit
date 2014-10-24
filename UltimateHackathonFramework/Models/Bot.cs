using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    class Bot : IBot
    {

        public string ID
        {
            get { throw new NotImplementedException(); }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
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
