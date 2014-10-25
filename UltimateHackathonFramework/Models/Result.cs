using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    class Result : IResult
    {
        private StringBuilder _results = new StringBuilder();
        private StringBuilder _log = new StringBuilder();
        public string Log
        {
            get { return _log.ToString(); }
        }


        public void addFinalResult(string result)
        {
            _results.AppendLine(result);
        }


        public void addToLog(string log)
        {
            _log.AppendLine(log);
        }

        public Result() { }




        public void addToLog(string action, Dictionary<string, string> dict)
        {
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                addToLog(action + " " + kvp.Key + " " + kvp.Value);
            }
        }


        public string Results
        {
            get { return _results.ToString(); }
        }
    }
}
