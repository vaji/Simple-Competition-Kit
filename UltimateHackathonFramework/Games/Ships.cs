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
        public Ships() :base("Ships") { }
        class Ship
        {
            public int i;
            public int j;
            public int type;
            public int direct;
            public int shot=0;
            
            public Ship (){}
            public Ship (int i, int j, int type, int direct)
            {
                this.i=i;
                this.j=j;
                this.type=type;
                this.direct=direct;
            }

            public bool isShotInShip(int i, int j)
            {
                int endI,endJ;
                switch(this.direct)
                {
                    case 0: endJ=this.j;
                            endI=this.i+this.type;
                            if((j==endJ)&&(this.i<=i)&&(i<=endI))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                    case 1: endJ=this.j+this.type;
                            endI=this.i;
                            if((i==endI)&&(this.j<=j)&&(j<=endJ))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                    case 2: endJ = this.j;
                            endI = this.i - this.type;
                            if((j==endJ)&&(this.i>=i)&&(i>=endI))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                    case 3: endJ = this.j - this.type;
                            endI = this.i;
                            if((i==endI)&&(this.j>=j)&&(j>=endJ))
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
                int endI, endJ;
                int iss=0, iee=0, jss=0, jee=0;
                switch (ship.direct)
                {
                    case 0: endJ = ship.j;
                            endI = ship.i + ship.type;
                            if (ship.i-1 < 0) iss = 0; else iss = ship.i-1;
                            if (endI+1 > 9) iee = 9; else iee = endI+1;
                            if (endJ - 1 < 0) jss = 0; else jee = endJ - 1;
                            if (endJ + 1 > 9) jee = 9; else jee = endJ + 1;
                            break;
                    case 1: endJ = ship.j + ship.type;
                            endI = ship.i;
                            if (ship.i-1 < 0) iss = 0; else iss = ship.i-1;
                            if (endI+1 > 9) iee = 9; else iee = endI+1;
                            if (ship.j - 1 < 0) jss = 0; else jee = ship.j - 1;
                            if (endJ + 1 > 9) jee = 9; else jee = endJ + 1;
                            break;
                    case 2: endJ = ship.j;
                            endI = ship.i - ship.type;
                            if (ship.i - 1 > 0) iss = 0; else iss = ship.i - 1;
                            if (endI + 1 < 0) iee = 0; else iee = endI - 1;
                            if (endJ - 1 < 0) jss = 0; else jee = endJ - 1;
                            if (endJ + 1 > 9) jee = 9; else jee = endJ + 1;
                            break;
                    case 3: endJ = ship.j - ship.type;
                            endI = ship.i;
                            if (endI-1 < 0) iss = 0; else iss = endI-1;
                            if (ship.i + 1 > 9) iee = 9; else iee = ship.i + 1;
                            if (ship.j + 1 > 9) jss = 9; else jee = ship.j + 1;
                            if (endJ - 1 < 0) jee = 0; else jee = endJ - 1;
                            break;
                }
                for (int i = iss; i <= iee;i++ )
                {
                    for(int j =jss; j<=jee;j++)
                    {
                        if(sea[index,i,j].CellState==CellStateEnum.Ship)
                        {
                            return false;
                        }
                    }
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


            Dictionary<string, string> XOMapper = new Dictionary<string, string>();
            int botNow=0;
            while(!isEnd())
            {
                Dictionary<string, string> response = botsList[botNow].Communicate(new Dictionary<string, string>() { { "shot", "" } });
                if (response.ContainsKey("action") && response["action"] =="shot")
                {
                    
                }
                else
                {
                    //wywalic bota
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
