using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Games
{
    public class WarGame : IGame
    {
        IClientManager ClientManager;
        IList<IBot> allBotsList;

        string testString = "hello";
        public bool jest_ok = false;
        public int wygrany_index = -1;
        public WarGame( IClientManager clientmng)
        {
            ClientManager = clientmng;
            allBotsList = ClientManager.Clients;
        }
        public WarGame() 
        {
          
        }
        public IResult Result
        {
            get { throw new NotImplementedException(); }
        }

        public void StartAll()
        {
                throw new NotImplementedException();
        }

        public void Start(IList<IBot> bots)
        {
            var botsLength = bots.Count;
             
            for (var i = 0; i < botsLength; i++)
            {
                if (bots[i].ID != null)
                {
                    if (!DoYouCopy(bots[i]))
                    {
                        bots.Remove(bots[i]);
                    }

                }

            }
            botsLength = bots.Count;
            Random rnd = new Random();

            IBot winnerBot = null;
            bool isThereAWinner = false;
            int winner_index = -1;
            if (botsLength > 1)
            {
                winner_index = rnd.Next(0, botsLength);
                winnerBot = bots[winner_index];
                isThereAWinner = true;
            }
            else if (botsLength == 1)
            {
                winnerBot = bots[0];
                isThereAWinner = true;
            }
            else
            {
                isThereAWinner = false;
                // no bots
            }

            if (isThereAWinner)
            {
                Dictionary<string, string> winData = new Dictionary<string,string>();
                Dictionary<string, string> resultData = new Dictionary<string, string>();
                winData.Add("win", "no siema");
                
                resultData = winnerBot.Communicate(winData);
                if (resultData.ContainsKey("win"))
                {
                    if (resultData["win"] == "no siema")
                    {
                        ResultsAvailable.Invoke();
                        jest_ok = true;
                        wygrany_index = winner_index;
                        //ok
                    }
                }
            }
        }

        private bool DoYouCopy(IBot bot)
        {
            Dictionary<string, string> helloDictionary = new Dictionary<string, string>();
            helloDictionary.Add("hello", testString);

            Dictionary<string, string> resultDictionary = new Dictionary<string, string>();

            resultDictionary = bot.Communicate(helloDictionary);
            if (resultDictionary.ContainsKey("hello"))
            {
                if (resultDictionary["hello"] == helloDictionary["hello"])
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }


        public int MinimumBots
        {
            get { throw new NotImplementedException(); }
        }

        public int MaximumBots
        {
            get { throw new NotImplementedException(); }
        }

        public event Action ResultsAvailable;


        public Models.ConfigRound getConfig()
        {
            throw new NotImplementedException();
        }


        public event EventHandler<System.ComponentModel.ProgressChangedEventArgs> ProgressChanged;
    }
}
