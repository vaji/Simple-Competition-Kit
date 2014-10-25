using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Interfaces
{
    public interface IGame
    {
        event Action ResultsAvailable;
        IResult Result { get; }

        void StartAll();
        void Start(IList<IBot> bots);

        int MinimumBots { get; }
        int MaximumBots { get;  }
    }
}
