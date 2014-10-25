using System.Collections.Generic;
using System.Windows;
using UltimateHackathonFramework.Interfaces;
namespace UltimateHackathonFramework {
    public class ResultsViewModel : Caliburn.Micro.PropertyChangedBase 
    {
        private IResult _results;

        public IResult RoundResults
        {
            get { return _results; }
            set {
                if (value == _results) return;
                  _results = value; 
                  NotifyOfPropertyChange(() => RoundResults);
                  NotifyOfPropertyChange(() => IsVisible);
            }
        }


        public Visibility IsVisible
        {
            get { return RoundResults != null && !IsBusy ? Visibility.Visible : Visibility.Collapsed; }
          
        }

       
        private int _percent;

        public int ProgressPercent
        {
            get { return _percent; }
            set
            {
                if (value == _percent) return;
                _percent = value;
                NotifyOfPropertyChange(() => ProgressPercent);
              
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set {
                if (value == _isBusy) return;
                _isBusy = value; 
                NotifyOfPropertyChange(() => IsBusy);
                NotifyOfPropertyChange(() => IsVisible);
            }
        }
    
    }
}