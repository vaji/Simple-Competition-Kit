using UltimateHackathonFramework.Interfaces;
namespace UltimateHackathonFramework {
    public class GameViewModel : Caliburn.Micro.PropertyChangedBase 
    {
        private ClientsViewModel _clientsViewModel;
        private ResultsViewModel _resultsViewModel;
        private IGame _game;
        public GameViewModel(IGame game, ClientsViewModel clientsViewModel)
        {
            _clientsViewModel = clientsViewModel;
            _clientsViewModel.PropertyChanged += (obj, arg) => NotifyOfPropertyChange(() => CanStart);
            _game = game;
            _game.ResultsAvailable += () => _resultsViewModel.RoundResults = _game.Result;
        }

        public void Start()
        {
            _game.Start(_clientsViewModel.SelectedBots);
        }
        public bool CanStart { 
            get { 
                    var botCount =_clientsViewModel.SelectedBots.Count;
                    return  botCount >= _game.MinimumBots && botCount <= _game.MaximumBots; 
            } 
        }
    }
}