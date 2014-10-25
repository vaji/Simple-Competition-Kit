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
        private string _message;
        public string Results
        {
            get { return _message; }
        }

        public RoundResult(string message)
        {
            _message = message;
        }
    }
}
