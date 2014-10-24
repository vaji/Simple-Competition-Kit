using System.Collections.Generic;
using UltimateHackathonFramework.Interfaces;
namespace UltimateHackathonFramework {
    public class ResultsViewModel : Caliburn.Micro.PropertyChangedBase 
    {
        private IResult _results;

        public IResult RoundResults
        {
            get { return _results; }
            set { _results = value; NotifyOfPropertyChange(() => RoundResults); }
        }
        

        
        
    }
}