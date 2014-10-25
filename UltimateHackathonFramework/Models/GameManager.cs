using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Games;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Models
{
    class GameManager : IGameManager
    {
        private IGame _round;
        private IClientManager _clientManager;
        private IResult _result;
        private IList<List<IBot>> _alternativelyCalculatedBots = new List<List<IBot>>();
        private IEnumerable<IEnumerable<IBot>> _calculatedBots;
        protected Mode _mode;

        public IResult Result
        {
            get { return _result; }
        }


        public GameManager() { }
        public GameManager(IClientManager clientManager)
        {
            _clientManager = clientManager;
            
            _availableGames = new List<IGame>() { new TicTacToe(), new Ships() };
            _round = _availableGames[0];

            _clientManager.ScanForClients(_round.Name);
        }

        public virtual void StartAll()
        {
            RunAsynchronously(RunAll);
        }

        private void RunAsynchronously(DoWorkEventHandler backgroundWorker_DoWork, object data = null)
        {
            _alternativelyCalculatedBots.Clear();
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
            if ((bots.Count > 0))

                CombinationUnique(new List<IBot>(), bots, -1, _round.Config.maxNumberBots);
            switch (_mode)
            {
                case Interfaces.Mode.NoRepeats:
                    _calculatedBots = _alternativelyCalculatedBots as IEnumerable<IEnumerable<IBot>>;
                    break;
                case Interfaces.Mode.AllCombinations:
                    _calculatedBots = CombinationNonUnique(_alternativelyCalculatedBots);
                    break;

            }

            int totalBots = _calculatedBots.Count();
            int progress = 0;

            foreach (var botToGo in _calculatedBots)
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

        private IEnumerable<IEnumerable<IBot>> CombinationNonUnique(IList<List<IBot>> _alternativelyCalculatedBots)
        {
            var bots = new List<List<IBot>>();
            foreach (var list in _alternativelyCalculatedBots)
            {
                foreach (var permutation in GetPermutations(list, list.Count()))
                    bots.Add(permutation.ToList());
            }
            return bots as IEnumerable<IEnumerable<IBot>>;
        }
        static IEnumerable<IEnumerable<T>>
          GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }


        void CombinationUnique(IList<IBot> tempBots, IList<IBot> bots, int lastValue, int digitsCount)
        {

            if (digitsCount > 0)
            {
                for (int it = ++lastValue; it != bots.Count; it++)
                {
                    tempBots.Add(bots[it]);
                    CombinationUnique(tempBots, bots, it, digitsCount - 1);
                    tempBots.RemoveAt(tempBots.Count - 1);
                }
            }
            else
            {
                _alternativelyCalculatedBots.Add(new List<IBot>());
                for (int i = 0; i < tempBots.Count; i++)
                {
                    _alternativelyCalculatedBots[_alternativelyCalculatedBots.Count - 1].Add(tempBots[i]);
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
            if (bots.Count <= _round.Config.maxNumberBots && bots.Count >= _round.Config.MinNumberBot)
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
        private List<IGame> _availableGames;

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


        public Mode Mode
        {
            set { _mode = value; }
        }


        public IGame Game
        {
            get
            {
                return _round;
            }
            set
            {
                _round = value;
            }
        }




        public List<IGame> Games
        { get { return _availableGames; } }
    }
}
