using UltimateHackathonFramework.Interfaces;
namespace UltimateHackathonFramework {
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell 
    {

        public IClientManager ClientManager
        {
            get;
            private set;
        }

        
        
    }
}