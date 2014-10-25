using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    class GameManager : IGame
    {
        private IRound _round;
        private IClientManager _clientManager;
        
        private IResult _result;
        private IList<List<IBot>> _botToGame = new List<List<IBot>>();

        public IResult Result
        {
            get { return _result; }
        }
        

        public GameManager() { }
        public GameManager(IRound round, IClientManager clientManager)
        {
            _round = round;
            _clientManager = clientManager;
            _clientManager.ScanForClients();

        }

        public virtual void StartAll()
        {
            var bots = _clientManager.Clients;
            _botToGame.Clear();
            _result = new Result();
            if ((_clientManager.Clients.Count > 0) && (_round.Config.EachOfEach))
            {
                Combination(new List<IBot>(), _clientManager.Clients, -1, _round.Config.maxNumberBots);
                foreach (List<IBot> botToGo in _botToGame)
                {
                    //_result.addResult(_round.Go(botToGo));
                    foreach (var bot in botToGo) bot.RunBot();
                    var result = _round.Go(botToGo);
                    _result.addResult(result);
                    foreach (var bot in botToGo) bot.KillBot();
                }
                foreach (IBot bot in _clientManager.Clients)
                {
                    Console.WriteLine(bot.Name + ": " + bot.Points);
                }
                OnResultsAvailable();
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
            /*
            foreach (var bot in bots) bot.RunBot();
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (obj, args) => _round.Go(bots);
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
            */
            
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
