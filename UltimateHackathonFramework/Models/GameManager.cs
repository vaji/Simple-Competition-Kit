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
            RunAsynchronously(RunAll);
        }

        private void RunAsynchronously(DoWorkEventHandler backgroundWorker_DoWork, object data = null)
        {
            _botToGame.Clear();
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += (obj, args) => OnResultsAvailable();
            backgroundWorker.ProgressChanged += (obj, args) => OnProgressChanged(args.ProgressPercentage);
            backgroundWorker.WorkerReportsProgress = true;
            if (data == null)
                backgroundWorker.RunWorkerAsync();
            else
                backgroundWorker.RunWorkerAsync(data);
        }

        void RunAll(object sender, DoWorkEventArgs e)
        {
            var bots = _clientManager.Clients;
            _result = new Result();
            var worker = sender as BackgroundWorker;
            if ((bots.Count > 0) && (_round.Config.EachOfEach))
            {
                Combination(new List<IBot>(), bots, -1, _round.Config.maxNumberBots);

                int totalBots = _botToGame.Count();
                int progress = 0;

                foreach (var botToGo in _botToGame)
                {
                    progress++;
                    foreach (var bot in botToGo) bot.RunBot();
                    var result = _round.Go(botToGo);
                    _result.addToLog(result.Results);
                    foreach (var bot in botToGo) bot.KillBot();
                    worker.ReportProgress(progress * 100 / totalBots);
                }
                foreach (IBot bot in _clientManager.Clients.OrderByDescending(x => x.Points))
                {
                    Console.WriteLine(bot.Name + ": " + bot.Points);
                    _result.addFinalResult(bot.Name + ": " + bot.Points);
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
            RunAsynchronously(RunSelected, bots);
            
        }

        private void RunSelected(object sender, DoWorkEventArgs e)
        {
            var bots = e.Argument as IList<IBot>;
            if (bots == null) throw new ArgumentException("Tried to launch game with something that is not a list of bots");
            _result = new Result();
            var worker = sender as BackgroundWorker;
            if (bots.Count <=  _round.Config.maxNumberBots && bots.Count >= _round.Config.MinNumberBot)
            {
                    worker.ReportProgress(10);
                    foreach (var bot in bots) bot.RunBot();
                    var result = _round.Go(bots);
                    _result.addToLog(result.Log);
                    _result.addFinalResult(result.Results);
                    foreach (var bot in bots) bot.KillBot();
                    worker.ReportProgress(90);
                
                foreach (IBot bot in _clientManager.Clients.OrderByDescending(x => x.Points))
                {
                    Console.WriteLine(bot.Name + ": " + bot.Points);
                    _result.addFinalResult(bot.Name + ": " + bot.Points + Environment.NewLine);
                    
                }
            }
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

        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        private void OnProgressChanged(int percent)
        {
            var args = new ProgressChangedEventArgs(percent, null);
            if (ProgressChanged != null)
                ProgressChanged(this, args);
        }

        public ConfigRound getConfig()
        {
            return _round.Config;
        }
    }
}
