using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UltimateHackathonFramework.Interfaces
{
    public interface IResult
    {
        string Log { get;  }

        string Results { get; }
        string getResultLog(int index);

        void addResult(IResult result);
        void addToLog(string log);
        void addToLog(string action, Dictionary<string, string> dict);

    }
}
