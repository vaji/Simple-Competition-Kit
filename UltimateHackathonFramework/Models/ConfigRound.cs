using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Models
{
    public class ConfigRound
    {
        private int _maxNumberBots;

        public int maxNumberBots
        {
            get { return _maxNumberBots; }
            set { _maxNumberBots = value; }
        }

        private int _numberOfRound;

        public int NumberOfRound
        {
            get { return _numberOfRound; }
            set { _numberOfRound = value; }
        }

        private int _minNumberBot;

        public int MinNumberBot
        {
            get { return _minNumberBot; }
            set { _minNumberBot = value; }
        }


        public ConfigRound() { NumberOfRound = 0; maxNumberBots = 2; MinNumberBot = 2; }
        
        
    }
}
