using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Interfaces
{
    public interface IGame
    {
        event Action ResultsAvailable;
        event EventHandler<ProgressChangedEventArgs> ProgressChanged;
        IResult Result { get; }

        void StartAll();
        void Start(IList<IBot> bots);

        Mode Mode { set; }

        UltimateHackathonFramework.Models.ConfigRound getConfig();
    }
}
