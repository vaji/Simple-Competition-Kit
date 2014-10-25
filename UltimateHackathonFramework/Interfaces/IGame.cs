using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Interfaces
{
    public interface IGameManager
    {
        event Action ResultsAvailable;
        event EventHandler<ProgressChangedEventArgs> ProgressChanged;
        IResult Result { get; }

        void StartAll();
        void Start(IList<IBot> bots);

        Mode Mode { set; }

        UltimateHackathonFramework.Models.ConfigRound getConfig();

        List<IGame> Games { get; }
        IGame Game { get; set; }

        
    }
}
