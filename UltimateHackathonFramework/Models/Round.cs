using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    class Round : IRound
    {
        protected ConfigRound _config;
   

        public ConfigRound Config
        {
            get { return _config; }
            set { _config = value; }
        }
        
        public IResult Go(IList<IBot> bots)
        {
            throw new NotImplementedException();
        }
    }
}
