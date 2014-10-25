
using Caliburn.Micro;
using System.Collections.Generic;
using UltimateHackathonFramework.Interfaces;
using UltimateHackathonFramework.Models;
namespace UltimateHackathonFramework
{
    public class ClientsViewModel : Caliburn.Micro.PropertyChangedBase
    {
        private IClientManager _clientManager;
        private IGameManager _gameManager;
        public ClientsViewModel(IClientManager clientManager, IGameManager gameManager)
        {
            _clientManager = clientManager;
            _gameManager = gameManager;
            Bots = new BindableCollection<IBot>();
            Bots.AddRange(clientManager.Clients);
            SelectedBots = new BindableCollection<IBot>();
        }

        public IObservableCollection<IBot> Bots { get; private set; }
        public IObservableCollection<IBot> SelectedBots { get; private set; }



        private IBot _selectedBotFromListOfAll;

        public IBot SelectedBotFromListOfAll
        {
            get { return _selectedBotFromListOfAll; }
            set { _selectedBotFromListOfAll = value; 
                NotifyOfPropertyChange(() => SelectedBotFromListOfAll);
                NotifyOfPropertyChange(() => CanAddBot);
            }
        }

        private IBot _selectedBotFromSelectedList;

        public IBot SelectedBotFromSelectedList
        {
            get { return _selectedBotFromSelectedList; }
            set { _selectedBotFromSelectedList = value;
                NotifyOfPropertyChange(() => SelectedBotFromSelectedList);
                NotifyOfPropertyChange(() => CanRemoveBot);
            }
        }


        public void AddBot() {
            if (CanAddBot)
            {
                SelectedBots.Add(SelectedBotFromListOfAll);
                NotifyOfPropertyChange(() => CanAddBot);
            }
        }
        public bool CanAddBot { get { return SelectedBotFromListOfAll != null && !SelectedBots.Contains(SelectedBotFromListOfAll) ; } }

        public void ClickAddBot() { AddBot(); }
        public void RemoveBot() { if(CanRemoveBot) SelectedBots.Remove(SelectedBotFromSelectedList); }
        public void ClickRemoveBot() { RemoveBot(); NotifyOfPropertyChange(() => CanRemoveBot); }
        public bool CanRemoveBot { get { return SelectedBotFromSelectedList != null;  } }


        public void ScanForClients()
        {
            _clientManager.ScanForClients(_gameManager.Game.Name);
            Bots.Clear();
            Bots.AddRange(_clientManager.Clients);
            SelectedBots.Clear();
            NotifyOfPropertyChange();
        }
    }
}
