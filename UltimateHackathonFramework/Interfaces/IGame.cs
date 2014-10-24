using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Interfaces
{
    public interface IGame
    {
        public IResult Result { get; }

        public void StartAll();
        public void Start(IList<IBot> bots);


    }
}
