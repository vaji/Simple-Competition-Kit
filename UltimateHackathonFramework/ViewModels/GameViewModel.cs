
using UltimateHackathonFramework.Interfaces;
using UltimateHackathonFramework.ViewModels;

namespace UltimateHackathonFramework
{
    public class GameViewModel : Caliburn.Micro.PropertyChangedBase
    {
        private ClientsViewModel _clientsViewModel;
        private ResultsViewModel _resultsViewModel;
        private CommunicationViewModel _communicationViewModel;
        private IGame _game;
        public GameViewModel(IGame game, ClientsViewModel clientsViewModel, CommunicationViewModel communicationViewModel)
        {
            _clientsViewModel = clientsViewModel;
            _clientsViewModel.PropertyChanged += (obj, arg) => NotifyOfPropertyChange(() => CanStart);
            _game = game;
            _game.ResultsAvailable += () => _resultsViewModel.RoundResults = _game.Result;
            _communicationViewModel = communicationViewModel;
        }
        public CommunicationViewModel CommunicationViewModel { get { return _communicationViewModel; } }

        public void Start()
        {
            _game.Start(_clientsViewModel.SelectedBots);
        }
        public bool CanStart
        {
            get
            {
                var botCount = _clientsViewModel.SelectedBots.Count;
                return botCount >= _game.MinimumBots && botCount <= _game.MaximumBots;
            }
        }
    }
}