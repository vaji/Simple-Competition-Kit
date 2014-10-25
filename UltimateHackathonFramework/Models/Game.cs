using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        
        private IResult _result;
        private IList<List<IBot>> _botToGame = new List<List<IBot>>();
        private IResult _result=new Result();

        public IResult Result
        {
            get { return _result; }
        }
        

        public Game() { }
        public Game(IRound round, IClientManager clientManager)
        {
            _round = round;
            _clientManager = clientManager;
            _clientManager.ScanForClients();

        }
        public IResult Result
        {
            get { return _result; }
        }

        public virtual void StartAll()
        {
            _botToGame.Clear();
            _result=new Result();
            if((_clientManager.Clients.Count>0) && (_round.Config.EachOfEach))
            {
                /*
                 * Narazie zostawmy to
                 * 
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
                }*/

                Combination(new List<IBot>(), _clientManager.Clients, -1, _round.Config.maxNumberBots);
                foreach(List<IBot> botToGo in _botToGame)
                {
                     _result.addResult(_round.Go(botToGo));
                }
             }
        }

        void Combination(IList<IBot> tempBots, IList<IBot> bots, int lastValue, int digitsCount)
        {
            if (digitsCount > 0)
            {
                for (int it = ++lastValue; it != bots.Count; it++)
                {
                    tempBots.Add(bots[it]);
                    Combination(tempBots, bots, it, digitsCount - 1);
                    tempBots.RemoveAt(tempBots.Count - 1);
                }
            }
            else
            {
                _botToGame.Add(new List<IBot>());
                for (int i = 0; i < tempBots.Count; i++ )
                {
                    _botToGame[_botToGame.Count - 1].Add(tempBots[i]);
                }
            }
        }
        

        public virtual void Start(IList<IBot> bots)
        {
            foreach (var bot in bots) bot.RunBot();
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (obj, args) => _round.Go(bots);
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();

        }

        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _result = e.Result as IResult;
        }

        private void OnResultsAvailable()
        {
            if (ResultsAvailable != null) ResultsAvailable();
        }

        public event Action ResultsAvailable;

        public ConfigRound getConfig()
        {
            return _round.Config;
        }
    }
}
