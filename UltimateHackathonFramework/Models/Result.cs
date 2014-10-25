﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    class Result : IResult
    {
        private List<IResult> _results;
        private StringBuilder _log = new StringBuilder();
        public string Log
        {
            get { return _log.ToString(); }
        }


        public void addResult(IResult result)
        {
            if(_results==null)
            {
                _results=new List<IResult>();
            }
            _results.Add(result);
        }


        public void addToLog(string log)
        {
            _log.AppendLine(log);
        }

        public Result() { }


        public string getResultLog(int index)
        {
            if((_results!=null)&&(index<_results.Count))
            {
                return _results[index].Log;
            }
            else
            {
                return null;
            }
        }
    }
}
