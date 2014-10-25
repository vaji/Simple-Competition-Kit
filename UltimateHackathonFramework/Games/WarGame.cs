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

        public WarGame( IClientManager clientmng)
        {
            ClientManager = clientmng;
            allBotsList = ClientManager.Clients;
        }
        public IResult Result
        {
            get { throw new NotImplementedException(); }
        }

        public void StartAll()
        {
            var botsLength = allBotsList.Count();
            
            for (var i = 0; i < botsLength; i++)
            {
                if (allBotsList[i].ID != null)
                {
                    Dictionary<string, string> tempDictionary = null;
                    tempDictionary.Add("temp", testString);

                    Dictionary<string, string> resultDictionary;
                    resultDictionary = allBotsList[i].Communicate(tempDictionary);
                  
                }
            }
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
            if (botsLength > 1)
            {
                int winner_index = rnd.Next(0, botsLength);
                winnerBot = bots[winner_index];
            }
            else if (botsLength == 1)
            {
                winnerBot = bots[0];
            }
            else
            { 
                // no bots
            }

            
        }

        private bool DoYouCopy(IBot bot)
        {
            Dictionary<string, string> helloDictionary = null;
            helloDictionary.Add("hello", testString);

            Dictionary<string, string> resultDictionary = null;

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
    }
}
