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
                IList<List<IBot>> botCouple = new List<List<IBot>>();
                int moveIndex = 0;
                for (int i = moveIndex; i < _clientManager.Clients.Count-1; i++ )
                {
                    for(int j=moveIndex+1; j< _clientManager.Clients.Count; j++)
                    {
                        botCouple.Add(new List<IBot>());
                        botCouple[botCouple.Count - 1].Add(_clientManager.Clients[i]);
                        botCouple[botCouple.Count - 1].Add(_clientManager.Clients[j]);
                    }
                    moveIndex++;
                }
                foreach(List<IBot> couple in botCouple)
                {
                    IResult tempR = _round.Go(couple);
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
