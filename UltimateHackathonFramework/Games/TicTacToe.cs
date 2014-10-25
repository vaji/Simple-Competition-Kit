using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;
using UltimateHackathonFramework.Models;

namespace UltimateHackathonFramework.Games
{
    struct field
    {
       public int numer;
       public bool taken;
       public string sign;
    }

    class TicTacToe : Round
    {
        string testString = "hello";
        bool game_finished = false;
        string victory_sign = "";
        field[] pola;
        int[,] win_lanes;
        IBot victory_bot;
        Dictionary<IBot, string> BotToSign = new Dictionary<IBot, string>();



        protected override IResult DoRound(IList<IBot> bots)
        {
            pola = new field[9];
            win_lanes = new int[8,3];
            game_finished = false;

            for (var i = 0; i < 9; i++)
            {
                pola[i] = new field();
                pola[i].numer = i;
                pola[i].taken = false;
                pola[i].sign = "";
            }

            win_lanes[0, 0] = 0; win_lanes[0, 1] = 1; win_lanes[0, 2] = 2;
            win_lanes[1, 0] = 3; win_lanes[1, 1] = 4; win_lanes[1, 2] = 5;
            win_lanes[2, 0] = 6; win_lanes[2, 1] = 7; win_lanes[2, 2] = 8;
            win_lanes[3, 0] = 0; win_lanes[3, 1] = 3; win_lanes[3, 2] = 6;
            win_lanes[4, 0] = 1; win_lanes[4, 1] = 4; win_lanes[4, 2] = 7;
            win_lanes[5, 0] = 2; win_lanes[5, 1] = 5; win_lanes[5, 2] = 8;
            win_lanes[6, 0] = 0; win_lanes[5, 1] = 4; win_lanes[5, 2] = 8;
            win_lanes[7, 0] = 2; win_lanes[5, 1] = 4; win_lanes[5, 2] = 6;


            var botsLength = bots.Count;

            for (var i = 0; i < botsLength; i++)
            {
                if (bots[i].ID != null)
                {
                    if(i == 0) testString = "O";
                    else testString = "X";

                    if (!DoYouCopy(bots[i]))
                    {
                        bots.Remove(bots[i]);
                    }
                }
            }

            botsLength = bots.Count;

            if (botsLength > 1)
            {
                int current_index = 0;
                int picked_field = -1;
                Dictionary<string, string> giveMeMoveDict = new Dictionary<string, string>();
                Dictionary<string, string> botMoveDict = new Dictionary<string, string>();
                Dictionary<string, string> botOrderDict = new Dictionary<string, string>();
                giveMeMoveDict.Add("move", "");

                while (!game_finished)
                {
                    botMoveDict = bots[current_index].Communicate(giveMeMoveDict);
                    if (botMoveDict.ContainsKey("move"))
                    {
                        picked_field = int.Parse(botMoveDict["move"]);
                        pola[picked_field].taken = true;
                        pola[picked_field].sign = BotToSign[bots[current_index]];
                    }

                    botOrderDict.Add(BotToSign[bots[current_index]],picked_field+"");

                    for (var i = 0; i < 2; i++)
                    {
                        bots[i].Communicate(botOrderDict);
                    }
                    if (CheckConditions()) break;
                    else
                    {
                        if (current_index == 0) current_index = 1;
                        else current_index = 0;
                    }
                }

                Dictionary<string, string> winInfoDict = new Dictionary<string, string>();
                winInfoDict.Add("win", victory_sign);

                for (var i = 0; i < 2; i++)
                {
                    bots[i].Communicate(winInfoDict);
                }

                
            }
            RoundResult result = new RoundResult(victory_bot.Name);
            return result;
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
                    BotToSign[bot] = resultDictionary["hello"];
                    return true;
                }
                else return false;
            }
            else return false;
        }

        private bool CheckConditions()
        {

            var temp_znak = "";
            bool condition_ok = true; 
                for (var i = 0; i < 8; i++)
                {
                    for (var s = 0; s < 3; s++)
                    {
                        if (s == 0) temp_znak = pola[win_lanes[i, s]].sign;
                        else
                        {
                            if (pola[win_lanes[i, s]].sign != temp_znak)  { condition_ok = false; break; }
                        }

                        if (s == 2 && condition_ok == true) break;
                    }
                }
                if (condition_ok)
                { victory_sign = temp_znak;
                victory_bot = BotToSign.Where(x => x.Value == temp_znak).First().Key;
                }
                return condition_ok;

        }
    }
}
