using UltimateHackathonFramework.Interfaces;
namespace UltimateHackathonFramework {
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell 
    {

        public ClientsViewModel ClientsViewModel
        {
            get;
            private set;
        }
        public ResultsViewModel ResultsViewModel { get; private set; }
        public GameViewModel GameViewModel { get; private set; }
        public ShellViewModel(ClientsViewModel clientsViewModel, GameViewModel gameViewModel, ResultsViewModel resultsViewModel)
        {
            ClientsViewModel = clientsViewModel;
            ResultsViewModel = resultsViewModel;
            GameViewModel = gameViewModel;
        }
        
        
    }
}