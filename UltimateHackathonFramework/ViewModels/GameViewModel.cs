
using UltimateHackathonFramework.Interfaces;
using UltimateHackathonFramework;
using System.Collections.Generic;
using UltimateHackathonFramework.Models;

namespace UltimateHackathonFramework
{
    public class GameViewModel : Caliburn.Micro.PropertyChangedBase
    {
        private ClientsViewModel _clientsViewModel;
        private ResultsViewModel _resultsViewModel;
        private CommunicationViewModel _communicationViewModel;
        private IGameManager _game;
        public GameViewModel(IGameManager game, ClientsViewModel clientsViewModel, CommunicationViewModel communicationViewModel, ResultsViewModel resultsViewModel)
        {
            _resultsViewModel = resultsViewModel;

            _clientsViewModel = clientsViewModel;
            _clientsViewModel.PropertyChanged += (obj, arg) =>
                {
                    NotifyOfPropertyChange(() => CanStart);
                    NotifyOfPropertyChange(() => CanStartAll);
                    NotifyOfPropertyChange(() => CanResetPoints);
                };

            _game = game;
            _game.ResultsAvailable += () =>
                {
                    _resultsViewModel.RoundResults = _game.Result;
                    _clientsViewModel.Bots.Refresh();
                    _clientsViewModel.SelectedBots.Refresh();
                };
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
                return botCount >= config.MinNumberBot && botCount <= config.MinNumberBot && !IsBusy && SelectedGame != null;
            }
        }
        public bool CanStartAll
        {
            get { return _clientsViewModel.Bots.Count > 0 && !IsBusy && SelectedGame != null; }
        }

        public void ResetPoints()
        {
            foreach (var bot in _clientsViewModel.Bots)
                bot.ClearPoints();
            _clientsViewModel.SelectedBots.Refresh();
        }
        public bool CanResetPoints
        { get { return !IsBusy && _clientsViewModel.Bots != null && _clientsViewModel.Bots.Count > 0; } }

        public List<Mode> Modes
        {
            get
            {
                return new List<Mode>() { Mode.AllCombinations, Mode.NoRepeats};
            }
        }
        private Mode _selectedMode;
        public Mode SelectedMode
        {
            get { return _selectedMode; }
            set { _selectedMode = value; _game.Mode = value; NotifyOfPropertyChange(() => SelectedMode); }
        }
        public List<Game> Games
        {
            get
            {
                return _game.Games.ConvertAll<Game>(x => x as Game);
            }
        }
        public IGame SelectedGame
        {
            get { return _game.Game; }
            set { _game.Game = value; 
                NotifyOfPropertyChange(() => SelectedGame); 
                NotifyOfPropertyChange(() => CanStart); 
                NotifyOfPropertyChange(() => CanStartAll); 
                _clientsViewModel.ScanForClients();
                NotifyOfPropertyChange(() => CanResetPoints);
            }
        }
    }
}