using UltimateHackathonFramework.Interfaces;
namespace UltimateHackathonFramework {
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell 
    {

        public ClientsViewModel ClientsViewModel
        {
            get;
            private set;
        }

        public ShellViewModel(ClientsViewModel clientsViewModel)
        {
            ClientsViewModel = clientsViewModel;
        }
        
        
    }
}