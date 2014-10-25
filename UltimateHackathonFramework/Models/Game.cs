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
                //for(int i=0; i<Cl)
            }
        }

        public virtual void Start(IList<IBot> bots)
        {
            throw new NotImplementedException();
        }

        public event Action ResultsAvailable;
    }
}
