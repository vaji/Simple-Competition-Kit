using System.Collections.Generic;
using UltimateHackathonFramework.Interfaces;
namespace UltimateHackathonFramework {
    public class ResultsViewModel : Caliburn.Micro.PropertyChangedBase 
    {
        private IRoundResult _results;

        public IRoundResult RoundResults
        {
            get { return _results; }
            set { _results = value; NotifyOfPropertyChange(() => RoundResults); }
        }
        

        
        
    }
}