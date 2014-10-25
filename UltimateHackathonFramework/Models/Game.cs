using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    public class Game : IGame
    {
        protected ConfigRound _config;
        protected IResult _result=new Result();
        protected string _name;
        public Game(string name)
        {
            _name = name;
            Config = new ConfigRound() { maxNumberBots = 2, MinNumberBot = 2 };
        }
        public ConfigRound Config
        {
            get { return _config; }
            set { _config = value; }
        }

        protected virtual IResult DoRound(IEnumerable<IBot> bots){ return null;}
        public IResult Go(IEnumerable<IBot> bots)
        {
            _result = new Result();
             var result =  DoRound(bots);
             return result;
            	 
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

        public string Name { get { return _name; } }
        public override string ToString()
        {
            return _name;
        }
    }
}
