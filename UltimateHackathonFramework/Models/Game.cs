using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    class Game : IGame
    {
        private IRound _round;
        private IClientManager _clientManager;

        public Game() { }
        public Game(IRound round)
        {
            _round = round;
            _clientManager=new ClientManager();
            _clientManager.ScanForClients();

        }
        public IResult Result
        {
            get { throw new NotImplementedException(); }
        }

        public virtual void StartAll()
        {
            if((_clientManager.Clients.Count>0) && (_round.Config.EachOfEach))
            {
                IList<List<IBot>> gameCouple = new List<List<IBot>>();
                for (int i = 0; i < _clientManager.Clients.Count; i++ )
                {

                }
                IList<IBot> tempBots = new List<IBot>();
                int moveIndeks = 0;
                for(int i=0; i<_clientManager.Clients.Count-1; i++)
                {
                    tempBots.Add(_clientManager.Clients[i]);
                    for(int j=moveIndeks; j<_clientManager.Clients.Count;j++)
                    {
                        if((i!=j))
                        {
                            tempBots.Add(_clientManager.Clients[j]);
                            
                        }
                    }
                }
            }
        }

        public virtual void Start(IList<IBot> bots)
        {
            throw new NotImplementedException();
        }

        public event Action ResultsAvailable;

        public ConfigRound getConfig()
        {
            return _round.Config;
        }
    }
}
