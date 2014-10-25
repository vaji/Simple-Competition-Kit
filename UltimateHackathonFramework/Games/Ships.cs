using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Models;
using UltimateHackathonFramework.Interfaces;

namespace UltimateHackathonFramework.Games
{
    public class Ships:Game
    {
        enum CellStateEnum { Miss, Ship, Sink, Shotted,ShottedShip};
        public Ships() { }
        class Ship
        {
            public int x;
            public int y;
            public int type;
            public int direct;
            public int shot=0;
            
            public Ship (){}
            public Ship (int x, int y, int type, int direct)
            {
                this.x=x;
                this.y=y;
                this.type=type;
                this.direct=direct;
            }

            public void sinkShip(Cell[,,] sea,int index)
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
                int endX,endY;
                switch(this.direct)
                {
                    case 0: endY=this.y;
                            endX=this.x+this.type;
                            if((y==endY)&&(this.x<=x)&&(x<=endX))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                    case 1: endY=this.y+this.type;
                            endX=this.x;
                            if((x==endX)&&(this.y<=y)&&(y<=endY))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                    case 2: endY = this.y;
                            endX = this.x - this.type;
                            if((y==endY)&&(this.x>=x)&&(x>=endX))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                    case 3: endY = this.y - this.type;
                            endX = this.x;
                            if((x==endX)&&(this.y>=y)&&(y>=endY))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                }
                return false;
            }
            public void attack()
            {
                this.shot++;
            }

            public bool isSink()
            {
                if(this.shot==this.type)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        class ShipManager
        {
            private IList<Ship> _shipCollection=new List<Ship>();
            public void addShip(Ship ship)
            {
                _shipCollection.Add(ship);
            }
            public Ship isShotInShip(int x, int y)
            {
                foreach(Ship s in _shipCollection)
                {
                    if(s.isShotInShip(x,y))  
                    {
                        return s;
                    }
                }
                return null;
            }

            public bool checkAddShip(Ship ship, Cell[,,] sea, int index)
            {
                int endX, endY;
                int iss=0, iee=0, jss=0, jee=0;
                switch (ship.direct)
                {
                    case 0: endX = ship.x;
                            endY = ship.y - ship.type;
                            if (ship.y+1 > 9) jss = 0; else jss = ship.y+1;
                            if (endY -1 < 0) jee = 0; else jee = endY-1;
                            if (endX - 1 < 0) iss = 0; else iee = endX - 1;
                            if (endX + 1 > 9) iee = 9; else iee = endX + 1;
                            break;
                    case 1: endX = ship.x + ship.type;
                            endY = ship.y;
                            if (ship.x-1 < 0) iss = 0; else iss = ship.x-1;
                            if (endX+1 > 9) iee = 9; else iee = endX+1;
                            if (ship.y - 1 < 0) jss = 0; else jee = ship.y - 1;
                            if (endY + 1 > 9) jee = 9; else jee = endY + 1;
                            break;
                    case 2: endX = ship.x;
                            endY = ship.y + ship.type;
                            if (ship.x - 1 < 0) iss = 0; else iss = ship.x - 1;
                            if (endX + 1 > 9) iee = 9; else iee = endX + 9;
                            if (ship.y - 1 < 0) jss = 0; else jee = ship.y - 1;
                            if (endY + 1 > 9) jee = 9; else jee = endY + 1;
                            break;
                    case 3: endX = ship.x - ship.type;
                            endY = ship.y;
                            if (endX-1 < 0) iss = 0; else iss = endX-1;
                            if (ship.x + 1 > 9) iee = 9; else iee = ship.x + 1;
                            if (ship.y + 1 > 9) jss = 9; else jee = ship.y + 1;
                            if (endY - 1 < 0) jee = 0; else jee = endY - 1;
                            break;
                }
                for (int i = iss; i < iee;i++ )
                {
                    for(int j =jss; j<jee;j++)
                    {
                        if(sea[index,j,i].CellState==CellStateEnum.Ship)
                        {
                            return false;
                        }
                    }
                }
                switch (ship.direct)
                {
                    case 0: for (int i = ship.y; i > ship.y-ship.type; i--)
                            {
                                sea[index, i, ship.x].CellState = CellStateEnum.Ship;    
                            }
                            break;
                    case 1: for (int i = ship.x; i < ship.x + ship.type; i++)
                            {
                                sea[index, ship.y, i].CellState = CellStateEnum.Ship;
                            } 
                            break;
                    case 2: for (int i = ship.y; i < ship.y + ship.type; i++)
                            {
                                sea[index, i, ship.x].CellState = CellStateEnum.Ship;
                            } 
                            break;
                    case 3: for (int i = ship.x; i > ship.x - ship.type; i--)
                            {
                                sea[index, ship.y, i].CellState = CellStateEnum.Ship;
                            }
                            break;
                }
                addShip(ship);
                return true; ;
            }
        }
        class Cell
        {
            public CellStateEnum CellState = CellStateEnum.Miss;
        }
        private Cell[,,] _botSea;
        private int[] _ships=new int[2];
        private IList<ShipManager> _shipManager=new List<ShipManager>();
        protected override IResult DoRound(IEnumerable<IBot> bots)
        {
            IList<IBot> botsList=bots.ToList();
            _botSea = new Cell[2,10, 10]; 
            _shipManager.Clear();
            _shipManager.Add(new ShipManager());
            _shipManager.Add(new ShipManager());
            for( int i=0; i<2; i++)
            {
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        _botSea[i,x,y]=new Cell();
                    }
                }
                _ships[i]=20;
            }
            
            for(int i=0; i<2; i++)
            {
                if (!initShips(botsList[i].Communicate(new Dictionary<string, string>() { { "getShips", "" } }), i))
                {
                    botsList[i].CurrentState = State.Failed;
                }
            }
            if (!((botsList[0].CurrentState != State.Failed) && (botsList[1].CurrentState != State.Failed)))
            {
                //wywalic exception
            }

            int botNow=0;
            int enemy=1;
            bool yourTurn = true;
            while(!isEnd())
            {
                if (yourTurn)
                {
                    yourTurn = false;
                    Dictionary<string, string> response = botsList[botNow].Communicate(new Dictionary<string, string>() { { "action", "aim" } });
                    if (response.ContainsKey("action") && response["action"] == "shot")
                    {
                        Ship target = _shipManager[enemy].isShotInShip(Int32.Parse(response["x"]), Int32.Parse(response["y"]));
                        if (target != null)
                        {
                            if ((_botSea[enemy, Int32.Parse(response["y"]), Int32.Parse(response["x"])].CellState != CellStateEnum.ShottedShip) && (_botSea[enemy, Int32.Parse(response["y"]), Int32.Parse(response["x"])].CellState != CellStateEnum.Sink))
                            {
                                yourTurn = true;
                                target.attack();
                                _ships[enemy]--;
                                if (target.isSink())
                                {
                                    response = botsList[botNow].Communicate(new Dictionary<string, string>() { { "action", "hittedAndSinked" } });
                                }
                                else
                                {
                                    response = botsList[botNow].Communicate(new Dictionary<string, string>() { { "action", "hitted" } });
                                }
                            }
                        }
                        else
                        {
                            response = botsList[botNow].Communicate(new Dictionary<string, string>() { { "action", "miss" } });
                        }
                    }
                    else
                    {
                        //wywalic bota
                    }
                }
                if(!yourTurn)
                {
                    yourTurn = true;
                    int temp = botNow;
                    botNow = enemy;
                    enemy = temp;
                }

            }
            IResult result=new Result();
            return result;
        }

        public bool checkShot(int x, int y, int enemy)
        {
            return false;
        }

        private bool initShips(Dictionary<string, string> dict, int index)
        {
            if(dict.ContainsKey("action") && dict["action"] =="ships")
            {
                if(!_shipManager[index].checkAddShip(new Ship( Int32.Parse(dict["x1"]) , Int32.Parse(dict["y1"]) , Int32.Parse(dict["length1"]) , Int32.Parse(dict["direction1"]) ),_botSea,index)) return false;
                if (!_shipManager[index].checkAddShip(new Ship(Int32.Parse(dict["x2"]), Int32.Parse(dict["y2"]), Int32.Parse(dict["length2"]), Int32.Parse(dict["direction10"])), _botSea, index)) return false;
                if (!_shipManager[index].checkAddShip(new Ship(Int32.Parse(dict["x3"]), Int32.Parse(dict["y3"]), Int32.Parse(dict["length3"]), Int32.Parse(dict["direction10"])), _botSea, index)) return false;
                if (!_shipManager[index].checkAddShip(new Ship(Int32.Parse(dict["x4"]), Int32.Parse(dict["y4"]), Int32.Parse(dict["length4"]), Int32.Parse(dict["direction10"])), _botSea, index)) return false;
                if (!_shipManager[index].checkAddShip(new Ship(Int32.Parse(dict["x5"]), Int32.Parse(dict["y5"]), Int32.Parse(dict["length5"]), Int32.Parse(dict["direction10"])), _botSea, index)) return false;
                if (!_shipManager[index].checkAddShip(new Ship(Int32.Parse(dict["x6"]), Int32.Parse(dict["y6"]), Int32.Parse(dict["length6"]), Int32.Parse(dict["direction10"])), _botSea, index)) return false;
                if (!_shipManager[index].checkAddShip(new Ship(Int32.Parse(dict["x7"]), Int32.Parse(dict["y7"]), Int32.Parse(dict["length7"]), Int32.Parse(dict["direction10"])), _botSea, index)) return false;
                if (!_shipManager[index].checkAddShip(new Ship(Int32.Parse(dict["x8"]), Int32.Parse(dict["y8"]), Int32.Parse(dict["length8"]), Int32.Parse(dict["direction10"])), _botSea, index)) return false;
                if (!_shipManager[index].checkAddShip(new Ship(Int32.Parse(dict["x9"]), Int32.Parse(dict["y9"]), Int32.Parse(dict["length9"]), Int32.Parse(dict["direction10"])), _botSea, index)) return false;
                if (!_shipManager[index].checkAddShip(new Ship(Int32.Parse(dict["x10"]), Int32.Parse(dict["y10"]), Int32.Parse(dict["length10"]), Int32.Parse(dict["direction10"])), _botSea, index)) return false;
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool isEnd()
        {
            if((_ships[0]==0)||(_ships[1]==0))
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
