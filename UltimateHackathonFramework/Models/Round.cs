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

        public Round()
        {
            Config = new ConfigRound() { maxNumberBots = 2, MinNumberBot = 2 };
        }
        public ConfigRound Config
        {
            get { return _config; }
            set { _config = value; }
        }

        protected virtual IResult DoRound(IList<IBot> bots){ return null;}
        public IResult Go(IList<IBot> bots)
        {
            return DoRound(bots);
        }
    }
}
