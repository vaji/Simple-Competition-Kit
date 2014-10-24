using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    class Game : IGame
    {

        public IResult Result
        {
            get { throw new NotImplementedException(); }
        }

        public void StartAll()
        {
            throw new NotImplementedException();
        }

        public void Start(IList<IBot> bots)
        {
            throw new NotImplementedException();
        }


        public int MinimumBots
        {
            get { throw new NotImplementedException(); }
        }

        public int MaximumBots
        {
            get { throw new NotImplementedException(); }
        }
    }
}
