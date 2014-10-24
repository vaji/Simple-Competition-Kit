using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Games
{
    public class WarGame : IGame
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
    }
}
