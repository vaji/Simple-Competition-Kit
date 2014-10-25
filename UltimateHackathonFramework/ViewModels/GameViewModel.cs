
using UltimateHackathonFramework.Interfaces;
using UltimateHackathonFramework;
using UltimateHackathonFramework.ViewModels;
using System.Collections.Generic;

namespace UltimateHackathonFramework
{
    public class GameViewModel : Caliburn.Micro.PropertyChangedBase
    {
        private ClientsViewModel _clientsViewModel;
        private ResultsViewModel _resultsViewModel;
        private CommunicationViewModel _communicationViewModel;
        private IGame _game;
        public GameViewModel(IGame game, ClientsViewModel clientsViewModel, CommunicationViewModel communicationViewModel, ResultsViewModel resultsViewModel)
        {
            _resultsViewModel = resultsViewModel;

            _clientsViewModel = clientsViewModel;
            _clientsViewModel.PropertyChanged += (obj, arg) =>
                {
                    NotifyOfPropertyChange(() => CanStart);
                    NotifyOfPropertyChange(() => CanStartAll);
                };

            _game = game;
            _game.ResultsAvailable += () => _resultsViewModel.RoundResults = _game.Result;
            _game.ProgressChanged += (obj, args) => _resultsViewModel.ProgressPercent = args.ProgressPercentage;

            _communicationViewModel = communicationViewModel;
            _communicationViewModel.PropertyChanged += (obj, args) => Refresh();
            
        }
        public CommunicationViewModel CommunicationViewModel { get { return _communicationViewModel; } }

        private void InitGames()
        {
            _communicationViewModel.StartListening();
            IsBusy = true;
            _resultsViewModel.ProgressPercent = 0;
        }
        private bool IsBusy
        { get { return _resultsViewModel.IsBusy; }
            set { if (_resultsViewModel.IsBusy == value) return;
            _resultsViewModel.IsBusy = value;
            Refresh();
            }
        }
        private void CloseGames()
        {
            _communicationViewModel.StopListening();
            IsBusy = false;
            _game.ResultsAvailable -= CloseGames;
        }
        public void Start()
        {
            InitGames();
            _game.Start(_clientsViewModel.SelectedBots);
            _game.ResultsAvailable += CloseGames;
        }
        public void StartAll()
        {

            InitGames();
            _game.StartAll();
            _game.ResultsAvailable += CloseGames;
            
        }
        public bool CanStart
        {
            get
            {
                var botCount = _clientsViewModel.SelectedBots.Count;
                var config = _game.getConfig();
                return botCount >= config.MinNumberBot  && botCount <= config.MinNumberBot && !IsBusy;
            }
        }
        public bool CanStartAll
        {
            get { return _clientsViewModel.Bots.Count > 0 && !IsBusy; }
        }

        public void ResetPoints()
        {
            foreach (var bot in _clientsViewModel.Bots)
                bot.ClearPoints();
        }
        public bool CanResetPoint
        { get { return !IsBusy; } }

        public List<IMode> Modes
        {
            get
            {
                return new List<IMode>() { };
            }
        }
    }
}