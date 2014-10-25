using System;
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
                  NotifyOfPropertyChange(() => CanSave);
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
                NotifyOfPropertyChange(() => CanSave);
            }
        }

        public void Save()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Results"; 
            dlg.DefaultExt = ".txt"; 
            dlg.Filter = "Text documents (.txt)|*.txt"; 

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                System.IO.File.WriteAllText(filename, RoundResults.Log + Environment.NewLine + RoundResults.Results);
            }
        }
        public bool CanSave { get { return !IsBusy && RoundResults != null; } }
    
    }
}