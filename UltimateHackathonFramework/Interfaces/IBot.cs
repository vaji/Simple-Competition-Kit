using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Interfaces
{
    public interface IBot
    {
        string ID { get; }
        string Name { get; }

        Dictionary<string,string> Communicate(Dictionary<string, string> data);

        void RunBot();
        void KillBot();
    }
}
