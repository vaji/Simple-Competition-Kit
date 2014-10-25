using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    class RoundResult : IResult
    {
        private StringBuilder _message;
        
        public RoundResult(string message)
        {
            _message.AppendLine(message);
        }

        public string Log
        {
            get { return _message.ToString(); }

        }

        public string getResultLog(int index)
        {
            throw new NotImplementedException();
        }
        
        public void addResult(IResult result)
        {
            throw new NotImplementedException();
        }

        public void addToLog(string log)
        {
            _message.AppendLine(log);
        }


        public void addToLog(string action, Dictionary<string, string> dict)
        {
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                addToLog(action + " " + kvp.Key + " " + kvp.Value);
            }
        }
    }
}
