using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;
using UltimateHackathonFramework.Models;

namespace UltimateHackathonFramework.Games
{

    
    enum CellStateEnum {Free, X, O};
    
    internal static class Helpers
    {
        public static char ToChar(this CellStateEnum state)
        {
            switch(state)
            {
                case CellStateEnum.Free: return ' ';
                case CellStateEnum.X: return 'X';
                case CellStateEnum.O: return 'O';
                default: return 'E';
            }
        }
    }

    class Cell
    {
        public CellStateEnum CellState = CellStateEnum.Free;
    }

    class TicTacToe: Game
    {
        public TicTacToe() :base("TicTacToe")
        {

        }
        Cell[,] Grid;
        private readonly int CELLX = 3;
        private readonly int CELLY = 3;

        protected override IResult DoRound(IEnumerable<IBot> bots)
        {
            Grid = new Cell[CELLX, CELLY];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    Grid[x, y] = new Cell();
            }
            }
            var botsList = bots.ToList();
            botsList[0].Communicate(new Dictionary<string, string>() { { "youAre", "X" } });
            botsList[1].Communicate(new Dictionary<string, string>() { { "youAre", "O" } });

            Dictionary<string, string> XOMapper = new Dictionary<string, string>();
            XOMapper.Add(botsList[0].ID, "X");
            XOMapper.Add(botsList[1].ID, "O");
            Result.addFinalResult("***********************");
            Result.addFinalResult(botsList[0].ID + " - X");
            Result.addFinalResult(botsList[1].ID + " - O");
            for (int iterator = 0; iterator < 9; iterator++)
            {
                int currentPlayer = iterator % 2;
                Dictionary<string, string> response = botsList[currentPlayer].Communicate(new Dictionary<string, string>() { { "move", "" } });
                _result.addToLog(botsList[currentPlayer].ID, response);
                if(response.ContainsKey("move") && response["move"]!="")
                {
                    int targetCell = -1;
                    try
                    {
                        targetCell = Int32.Parse(response["move"]);
                    }
                    catch (Exception)
                    {
                        botsList[currentPlayer].CurrentState = State.Failed;
                        botsList[currentPlayer].Communicate(new Dictionary<string, string>() { { "error", "TargetCellInfoCorrupted" } });
                        botsList[(iterator + 1) % 2].AddPoints(2);
                        break;
                    }
                    if(targetCell>8)
                    {
                        botsList[currentPlayer].CurrentState = State.Failed;
                        botsList[currentPlayer].Communicate(new Dictionary<string, string>() { { "error", "TargetCellOutOfRange" } });
                        botsList[(iterator + 1) % 2].AddPoints(2);
                        break;

                    }
                    int x = targetCell % 3;
                    int y = targetCell / 3;
                    if(Grid[x, y].CellState==CellStateEnum.Free)
                    {
                        if (XOMapper[botsList[iterator % 2].ID] == "X")
                        {
                            Grid[x, y].CellState = CellStateEnum.X;
                        }
                        if (XOMapper[botsList[iterator % 2].ID] == "O")
                        {
                            Grid[x, y].CellState = CellStateEnum.O;
                        }

                        foreach (Bot bot in botsList)
                        {
                            bot.Communicate(response);
                        }
                    }
                    else
                    {
                        botsList[currentPlayer].CurrentState = State.Failed;
                        botsList[currentPlayer].Communicate(new Dictionary<string, string>() { { "error", "TargetedCell is already taken!" } });
                        botsList[(iterator + 1) % 2].AddPoints(2);
                        break;
                    }
                    CellStateEnum status = verifyVictory(Grid);
                    if(status!=CellStateEnum.Free)
                    {
                        Result.addFinalResult(PrintGrid(Grid));
                        if(status==CellStateEnum.O)
                        {
                            foreach (Bot bot in botsList)
                            {
                                bot.Communicate(new Dictionary<string,string>(){{"win", "O"}});
                                if (XOMapper[bot.ID] == "O")
                                {
                                    Result.addFinalResult(bot.ID + " won as 0");
                                    bot.AddPoints(3);
                                }
                            }
                        }
                        if (status == CellStateEnum.X)
                        {
                            foreach (Bot bot in botsList)
                            {
                                bot.Communicate(new Dictionary<string, string>() { { "win", "X" } });
                                if (XOMapper[bot.ID] == "X")
                                {
                                    Result.addFinalResult(bot.ID + " won as X");
                                    bot.AddPoints(3);
                                }

                            }
                        }
                        break;
                    }
                    else 
                    {
                        int count = 0;
                        for (int ix = 0; ix < 3; ix++)
                        {
                            for (int iy = 0; iy < 3; iy++)
                            {
                                if (Grid[ix, iy].CellState == CellStateEnum.Free)
                                    count++;
                            }
                        }
                        if(count==0)
                        {
                            foreach (Bot bot in botsList)
                            {
                                bot.Communicate(new Dictionary<string, string>() { { "tie", "" } });
                                bot.AddPoints(1);
                                
                            }
                            Result.addFinalResult(PrintGrid(Grid));
                            Result.addFinalResult("Draw.");
                            break;
                        }
                    }
                }
                else
                {
                    botsList[iterator % 2].CurrentState = State.Failed;
                    botsList[iterator % 2].Communicate(new Dictionary<string, string>() { { "error", "CorruptedMoveResponseException" } });
                    botsList[(iterator + 1) % 2].AddPoints(2);
                    break;
                }
            }
            return Result;
        }

        private string PrintGrid(Cell[,] Grid)
        {
            var sb = new StringBuilder();
            for(int i = 0; i < CELLY; i++)
            {
                sb.AppendLine();
                sb.Append("|");
                for (int j = 0; j < CELLX; j++)
                {
                    sb.Append(Grid[j, i].CellState.ToChar());
                    sb.Append("|");
                }


            }
            sb.AppendLine();
            return sb.ToString();
        }

        private CellStateEnum verifyVictory(Cell[,] Grid)
        {
            for (int i = 0; i < 3; i++)
                {
                if (Grid[0, i].CellState == Grid[1, i].CellState && Grid[0, i].CellState == Grid[2, i].CellState && Grid[0, i].CellState != CellStateEnum.Free) return Grid[0, i].CellState;
                if (Grid[i, 0].CellState == Grid[i, 1].CellState && Grid[i, 0].CellState == Grid[i, 2].CellState && Grid[i, 0].CellState != CellStateEnum.Free) return Grid[i, 0].CellState; 
                }
            if (Grid[0, 0].CellState == Grid[1, 1].CellState && Grid[0, 0].CellState == Grid[2, 2].CellState && Grid[0, 0].CellState != CellStateEnum.Free) return Grid[0, 0].CellState;
            if (Grid[0, 2].CellState == Grid[1, 1].CellState && Grid[0, 2].CellState == Grid[2, 0].CellState && Grid[0, 2].CellState != CellStateEnum.Free) return Grid[0, 2].CellState;

            return CellStateEnum.Free;
            }
        }

  
}
