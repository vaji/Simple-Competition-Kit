using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework
{
    public class CommunicationViewModel : PropertyChangedBase
    {
        private ICommunication _communication;
        public CommunicationViewModel(ICommunication communication)
        {
            _communication = communication;
            _communication.StateChanged += () => 
            { 
                NotifyOfPropertyChange(() => Status);
                NotifyOfPropertyChange(() => CanStartListening);
                NotifyOfPropertyChange(() => CanStopListening); 
            };
            Port ="8000";
            IP = "127.0.0.1";
           
        }

        private string _ip;
        private string _port;
        public string IP
        {
            get { return _ip; }
            set { _ip = value; NotifyOfPropertyChange(() => IP); }
        }

        public string Status { get { return _communication.ServerState.ToString(); } }
        public string Port
        {
            get { return _port; }
            set { _port = value; NotifyOfPropertyChange(() => Port); }
        }
        
        public void StartListening()
        {
            _communication.StartListening(IP, Int32.Parse(Port));
        }
        public void StopListening()
        {
            _communication.StopListening();
        }
        public bool CanStartListening { get { return !String.IsNullOrWhiteSpace(IP) && !String.IsNullOrWhiteSpace(Port) && _communication.ServerState != ServerStateEnum.Listening; } }
        public bool CanStopListening { get { return _communication.ServerState == ServerStateEnum.Listening; } }
    }
}
