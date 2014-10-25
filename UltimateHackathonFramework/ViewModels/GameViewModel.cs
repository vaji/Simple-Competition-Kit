
using UltimateHackathonFramework.Interfaces;
using UltimateHackathonFramework;

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
            _communicationViewModel = communicationViewModel;
            _communicationViewModel.PropertyChanged += (obj, args) => Refresh();
        }
        public CommunicationViewModel CommunicationViewModel { get { return _communicationViewModel; } }

        public void Start()
        {
            _game.Start(_clientsViewModel.SelectedBots);
        }
        public void StartAll()
        {
            _game.StartAll();
        }
        public bool CanStart
        {
            get
            {
                var botCount = _clientsViewModel.SelectedBots.Count;
                var config = _game.getConfig();
                return botCount >= config.MinNumberBot  && botCount <= config.MinNumberBot && _communicationViewModel.Status == "Listening";
            }
        }
        public bool CanStartAll
        {
            get { return _clientsViewModel.Bots.Count > 0 && _communicationViewModel.Status == "Listening";}
        }
    }
}