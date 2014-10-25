using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    class Game : IRound
    {
        protected ConfigRound _config;
        protected IResult _result=new Result();
        
        public Game()
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
            IResult resut =  DoRound(bots);

            foreach (Bot bot in bots)
                {
                bot.KillBot();		 
        }
            return resut;
                }
        

        public IResult Result
        {
            get { return _result; }
        }




        private void OnResultsAvailable()
        {
            if (ResultsAvailable != null) ResultsAvailable();
        }

        public event Action ResultsAvailable;

        public ConfigRound getConfig()
        {
            return Config;
        }
    }
}
