using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Models;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Games
{
    public class Ships : Game
    {
        enum CellStateEnum { Miss, Ship, Sink, Shotted, ShottedShip };
        public Ships() : base("Ships") { }
        class Ship
        {
            public int x;
            public int y;
            public int type;
            public int direct;
            public int shot = 0;
            List<Tuple<int, int, CellStateEnum>> wspolrzedne = new List<Tuple<int, int, CellStateEnum>>();

            public Ship() { }
            public Ship(int x, int y, int type, int direct)
            {
                this.x = x;
                this.y = y;
                this.type = type;
                this.direct = direct;
                switch (direct)
                {
                    case 0: for (int i = 0; i < type; i++)
                        {
                            wspolrzedne.Add(new Tuple<int, int, CellStateEnum>(x, y - i, CellStateEnum.Ship));
                        }
                        break;
                    case 1: for (int i = 0; i < type; i++)
                        {
                            wspolrzedne.Add(new Tuple<int, int, CellStateEnum>(x + i, y, CellStateEnum.Ship));
                        } break;
                    case 2: for (int i = 0; i < type; i++)
                        {
                            wspolrzedne.Add(new Tuple<int, int, CellStateEnum>(x, y + i, CellStateEnum.Ship));
                        }
                        break;
                    case 3: for (int i = 0; i < type; i++)
                        {
                            wspolrzedne.Add(new Tuple<int, int, CellStateEnum>(x - i, y, CellStateEnum.Ship));
                        }
                        break;
                }
            }

            public bool contains(Ship ship, List<Ship> list)
            {
                foreach (Ship _ship in list)
                {
                    if (ship.intersects(_ship)) return true;
                }
                return false;
            }

            internal bool intersects(Ship _ship)
            {
                Console.WriteLine("2b");

                Tuple<int, int> p2 = getEndPoint();
                if (checkIfExceedsBoundaries(p2))
                    return true;

                Tuple<int, int> p2a = _ship.getEndPoint();
                if (cross(this.x, this.y, p2.Item1, p2.Item2, _ship.x, _ship.y, p2a.Item1, p2a.Item2)) return true;

                if (cross(this.x - 1, this.y - 1, p2.Item1 - 1, p2.Item2 + 1, _ship.x, _ship.y, p2a.Item1, p2a.Item2)) return true;
                if (cross(this.x - 1, this.y - 1, p2.Item1 + 1, p2.Item2 - 1, _ship.x, _ship.y, p2a.Item1, p2a.Item2)) return true;
                if (cross(this.x - 1, this.y + 1, p2.Item1 + 1, p2.Item2 + 1, _ship.x, _ship.y, p2a.Item1, p2a.Item2)) return true;
                if (cross(this.x + 1, this.y - 1, p2.Item1 + 1, p2.Item2 + 1, _ship.x, _ship.y, p2a.Item1, p2a.Item2)) return true;
                return false;
            }

            static bool cross(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
            {
                float c, d, e, f, g, h, u1, u2;
                c = x3 - x1;
                d = x3 - x4;
                e = x2 - x1;
                f = y3 - y1;
                g = y3 - y4;
                h = y2 - y1;
                if (e * g - d * h != 0 && h * d - e * g != 0)
                {
                    u1 = (e * f - c * h) / (e * g - d * h);
                    u2 = (d * f - c * g) / (h * d - e * g);
                }
                else
                {
                    return false;
                }
                if (u1 >= 0 && u1 <= 1 && u2 >= 0 && u2 <= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            private bool checkIfExceedsBoundaries(Tuple<int, int> p2)
            {

                if (p2.Item1 < 0 || p2.Item1 > 9 || p2.Item2 < 0 || p2.Item2 > 9)
                    return true;
                else
                    return false;
            }

            Tuple<int, int> getEndPoint()
            {
                int x2 = 0, y2 = 0;
                switch (direct)
                {
                    case 0:
                        {
                            x2 = x;
                            y2 = y - type;
                            break;
                        }
                    case 1:
                        {
                            x2 = x + type;
                            y2 = y;
                            break;
                        }
                    case 2:
                        {
                            x2 = x;
                            y2 = y + type;
                            break;
                        }
                    case 3:
                        {
                            x2 = x - type;
                            y2 = y;
                            break;
                        }

                    default:
                        break;
                }
                return new Tuple<int, int>(x2, y2);
            }

            public void sinkShip(Cell[, ,] sea, int index)
            {
                switch (this.direct)
                {
                    case 0: for (int i = this.y; i > this.y - this.type; i--)
                        {
                            sea[index, i, this.x].CellState = CellStateEnum.Sink;
                        }
                        break;
                    case 1: for (int i = this.x; i < this.x + this.type; i++)
                        {
                            sea[index, this.y, i].CellState = CellStateEnum.Sink;
                        }
                        break;
                    case 2: for (int i = this.y; i < this.y + this.type; i++)
                        {
                            sea[index, i, this.x].CellState = CellStateEnum.Sink;
                        }
                        break;
                    case 3: for (int i = this.x; i > this.x - this.type; i--)
                        {
                            sea[index, this.y, i].CellState = CellStateEnum.Sink;
                        }
                        break;
                }
            }
            public bool isShotInShip(int x, int y)
            {
                //foreach(Tuple<int,int,CellStateEnum> t in wspolrzedne)
                //{
                // if(t.Item1==x && t.Item2==y)
                // {
                // return true;
                // }
                //}
                return false;
                /*
                int endX,endY;
                switch(this.direct)
                {
                case 0: endX = this.x;
                endY=this.y-this.type;
                if((x==endX)&&(this.y<=y)&&(y<=endY))
                {
                return true;
                }
                else
                {
                return false;
                }
                case 1: endX=this.x+this.type;
                endY=this.y;
                if((y==endY)&&(this.x<=x)&&(x<=endX))
                {
                return true;
                }
                else
                {
                return false;
                }
                case 2: endX = this.x;
                endY = this.y + this.type;
                if((x==endX)&&(this.y<=y)&&(y<=endY))
                {
                return true;
                }
                else
                {
                return false;
                }
                case 3: endX = this.x - this.type;
                endY = this.y;
                if((y==endY)&&(this.x>=x)&&(x>=endX))
                {
                return true;
                }
                else
                {
                return false;
                }
                }
                return false;*/
            }
            public void attack()
            {
                this.shot++;
            }

            public bool isSink()
            {
                if (this.shot == this.type)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            internal void markShip(int index, Cell[, ,] _botSea)
            {
                Tuple<int, int> ends = this.getEndPoint();
                if (ends.Item1 == this.x)
                {
                    if (this.y >= ends.Item2)
                    {
                        for (int i = ends.Item2; i <= this.y; i++)
                        {
                            _botSea[index, i, this.x].CellState = CellStateEnum.Ship;
                        }
                    }
                    else
                    {
                        for (int i = this.y; i <= ends.Item2; i++)
                        {
                            _botSea[index, i, this.x].CellState = CellStateEnum.Ship;
                        }
                    }
                }
                else if (this.y == ends.Item2)
                {
                    if (this.x >= ends.Item1)
                    {
                        for (int i = ends.Item1; i <= this.x; i++)
                        {
                            _botSea[index, this.y, i].CellState = CellStateEnum.Ship;
                        }
                    }
                    else
                    {
                        for (int i = this.x; i <= ends.Item1; i++)
                        {
                            _botSea[index, this.y, i].CellState = CellStateEnum.Ship;
                        }
                    }
                }
            }
        }

        class ShipManager
        {
            private IList<Ship> _shipCollection = new List<Ship>();
            public void addShip(Ship ship)
            {
                _shipCollection.Add(ship);
            }

            public Ship isShotInShip(int x, int y)
            {
                foreach (Ship s in _shipCollection)
                {
                    if (s.isShotInShip(x, y))
                    {
                        return s;
                    }
                }
                return null;
            }

            public bool checkAddShip()
            {

                for (int i = 0; i < _shipCollection.Count; i++)
                {
                    List<Ship> temp = new List<Ship>();
                    for (int j = 0; j < _shipCollection.Count; j++)
                    {
                        if (i != j)
                        {
                            temp.Add(_shipCollection[j]);
                        }
                        if (_shipCollection[i].contains(_shipCollection[i], temp))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }


            internal void marks(int i, Cell[, ,] _botSea)
            {
                foreach (Ship s in _shipCollection)
                {
                    s.markShip(i, _botSea);
                }
            }
        }
        class Cell
        {
            public CellStateEnum CellState = CellStateEnum.Miss;
        }
        private Cell[, ,] _botSea;
        private int[] _ships = new int[2];
        private IList<ShipManager> _shipManager = new List<ShipManager>();
        protected override IResult DoRound(IEnumerable<IBot> bots)
        {
            IList<IBot> botsList = bots.ToList();
            _botSea = new Cell[2, 10, 10];
            _shipManager.Clear();
            _shipManager.Add(new ShipManager());
            _shipManager.Add(new ShipManager());
            for (int i = 0; i < 2; i++)
            {
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        _botSea[i, x, y] = new Cell();
                    }
                }
                _ships[i] = 20;
            }

            for (int i = 0; i < 2; i++)
            {
                Result.addToLog("Send", new Dictionary<string, string>() { { "getShips", "" } });
                if (!initShips(botsList[i].Communicate(new Dictionary<string, string>() { { "getShips", "" } }), i))
                {
                    botsList[i].CurrentState = State.Failed;
                }
            }
            if (!((botsList[0].CurrentState != State.Failed) && (botsList[1].CurrentState != State.Failed)))
            {
                //wywalic exception
            }

            for (int i = 0; i < 2; i++)
            {
                _shipManager[i].marks(i, _botSea);
            } 
            Dictionary<string, string> response;
            int botNow = 0;
            int enemy = 1;
            bool yourTurn = true;
            while (!isEnd())
            {
                if (yourTurn)
                {
                    yourTurn = false;
                    Result.addToLog("Send", new Dictionary<string, string>() { { "action", "aim" } });
                    response = botsList[botNow].Communicate(new Dictionary<string, string>() { { "action", "aim" } });
                    if (response.ContainsKey("action") && response["action"] == "noTarget")
                    {
                        Result.addToLog("Receive", response);
                    }
                    if (response.ContainsKey("action") && response["action"] == "shoot")
                    {
                            Result.addToLog("Receive", response);
                            if ((_botSea[enemy, Int32.Parse(response["y"]), Int32.Parse(response["x"])].CellState == CellStateEnum.Ship))
                            {
                                yourTurn = true;
                                _ships[enemy]--;
                                response = botsList[botNow].Communicate(new Dictionary<string, string>() { { "action", "hitted" } });
                                Result.addToLog("Send", response);
                            }
                            else
                            {
                                response = botsList[botNow].Communicate(new Dictionary<string, string>() { { "action", "missed" } });
                                Result.addToLog("Send", response);
                            }
                    }
                    else
                    {
                        botsList[botNow].CurrentState = State.Failed;
                    }
                }
                if (!yourTurn)
                {
                    yourTurn = true;
                    int temp = botNow;
                    botNow = enemy;
                    enemy = temp;
                }

            }
            string winner = "";
            if (_ships[0] == 0)
            {
                botsList[1].AddPoints(3);
                winner = botsList[1].Name;
                Result.addFinalResult(botsList[1].ID + " won");
            }
            else if (_ships[1] == 0)
            {
                botsList[0].AddPoints(3);
                winner = botsList[0].Name;
                Result.addFinalResult(botsList[0].ID + " won");
            }
            foreach(IBot bot in botsList)
            {
                response = bot.Communicate(new Dictionary<string, string>() { { "winner", winner } });
            }
            IResult result = new Result();
            return result;
        }

        public bool checkShot(int x, int y, int enemy)
        {
            return false;
        }

        private void finalResualt(IList<IBot> bots)
        {
            if(_ships[0]==0)
            {
                bots[1].AddPoints(3);
            }
            else if(_ships[1]==0)
            {
                bots[0].AddPoints(3);
            }
        }

        private bool initShips(Dictionary<string, string> dict, int index)
        {
            if (dict.ContainsKey("action") && dict["action"] == "ships")
            {
                Result.addToLog("Receive",dict);
                _shipManager[index].addShip(new Ship(Int32.Parse(dict["x1"]), Int32.Parse(dict["y1"]), Int32.Parse(dict["length1"]), Int32.Parse(dict["direction1"])));
                _shipManager[index].addShip(new Ship(Int32.Parse(dict["x2"]), Int32.Parse(dict["y2"]), Int32.Parse(dict["length2"]), Int32.Parse(dict["direction2"])));
                _shipManager[index].addShip(new Ship(Int32.Parse(dict["x3"]), Int32.Parse(dict["y3"]), Int32.Parse(dict["length3"]), Int32.Parse(dict["direction3"])));
                _shipManager[index].addShip(new Ship(Int32.Parse(dict["x4"]), Int32.Parse(dict["y4"]), Int32.Parse(dict["length4"]), Int32.Parse(dict["direction4"])));
                _shipManager[index].addShip(new Ship(Int32.Parse(dict["x5"]), Int32.Parse(dict["y5"]), Int32.Parse(dict["length5"]), Int32.Parse(dict["direction5"])));
                _shipManager[index].addShip(new Ship(Int32.Parse(dict["x6"]), Int32.Parse(dict["y6"]), Int32.Parse(dict["length6"]), Int32.Parse(dict["direction6"])));
                _shipManager[index].addShip(new Ship(Int32.Parse(dict["x7"]), Int32.Parse(dict["y7"]), Int32.Parse(dict["length7"]), Int32.Parse(dict["direction7"])));
                _shipManager[index].addShip(new Ship(Int32.Parse(dict["x8"]), Int32.Parse(dict["y8"]), Int32.Parse(dict["length8"]), Int32.Parse(dict["direction8"])));
                _shipManager[index].addShip(new Ship(Int32.Parse(dict["x9"]), Int32.Parse(dict["y9"]), Int32.Parse(dict["length9"]), Int32.Parse(dict["direction9"])));
                _shipManager[index].addShip(new Ship(Int32.Parse(dict["x10"]), Int32.Parse(dict["y10"]), Int32.Parse(dict["length10"]), Int32.Parse(dict["direction10"])));

                if (_shipManager[index].checkAddShip())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool isEnd()
        {
            if ((_ships[0] == 0) || (_ships[1] == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}