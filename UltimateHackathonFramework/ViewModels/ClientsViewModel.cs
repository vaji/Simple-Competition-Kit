using UltimateHackathonFramework.Interfaces;
namespace UltimateHackathonFramework {
    public class ClientsViewModel : Caliburn.Micro.PropertyChangedBase 
    {
        private IClientManager _clientManager;
        public ClientsViewModel(IClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        
        
    }
}