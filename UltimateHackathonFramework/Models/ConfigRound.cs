using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Models
{
    class ConfigRound
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
            get { return NumberOfRound; }
            set { NumberOfRound = value; }
        }

        private bool _eachOfEach;

        public bool EachOfEach
        {
            get { return _eachOfEach; }
            set { _eachOfEach = value; }
        }

        private int _minNumberBot;

        public int MinNumberBot
        {
            get { return _minNumberBot; }
            set { _minNumberBot = value; }
        }


        public ConfigRound() { EachOfEach = true; NumberOfRound = 0; maxNumberBots = 2; MinNumberBot = 2; }
        
        
    }
}
