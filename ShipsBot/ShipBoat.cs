using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShipsBot
{
    public class ShipBoat
    {
        public Communication comm;

        List<Ship> ships = new List<Ship>();

        int[,] table = new int[10, 10];

        Tuple<int, int, int> target;

        internal void Init()
        {
            setupShips();

        }

        private void setupShips()
        {
            Random r = new Random();

            for (int i = 0; i < 10; i++)
            {
                Ship ship = new Ship();
                if (i == 0) ship._length = 4;
                if (i > 0 && i < 3) ship._length = 3;
                if (i > 2 && i < 6) ship._length = 2;
                if (i > 5) ship._length = 1;
                ships.Add(ship);
            }
            Console.WriteLine("01");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("02");

                do{
                ships[i]._orientation = r.Next(4);
                ships[i]._X = r.Next(10);
                ships[i]._Y = r.Next(10);
                Console.WriteLine(i + " " + ships.Count);
                Console.WriteLine(i.ToString() + ":   x: " + ships[i]._X.ToString() + "   y: " + ships[i]._Y.ToString() + "   dir: " + ships[i]._orientation.ToString());
                } while ((ships[i].checkIfExceedsBoundaries(ships[i].getEndPoint())) || (i != 0 && contains(ships[i], ships.GetRange(0, i - 1))));
                Console.WriteLine("03");
            }
            Console.WriteLine("04");
        }

        private bool contains(Ship ship, List<Ship> list)
        {
            Console.WriteLine("2a");
            foreach (Ship _ship in list)
            {
                if(ship.intersects(_ship))return true;
            }
            return false;
        }

        internal Dictionary<string, string> consume(Dictionary<string, string> dict)
        {
            if(dict.ContainsKey("getShips"))
            {
                return buildShipsDictionary();
            }

            if (dict.ContainsKey("action") && dict["action"]=="aim")
            {
                return aimAndShoot();
            }

            if (dict.ContainsKey("action") && dict["action"] == "missed")
            {
                return missed();
            }

            if (dict.ContainsKey("action") && dict["action"] == "hitted")
            {
                return hitted();
            }

            if (dict.ContainsKey("action") && dict["action"] == "hittedAndSinked")
            {
                return hittedAndSinked();
            }

            return new Dictionary<string, string>();
        }

        private Dictionary<string, string> hittedAndSinked()
        {
            markSinkedFloodFill(target.Item1, target.Item2);
            
            return new Dictionary<string, string>();
        }

        private void markSinkedFloodFill(int x, int y)
        {
            table[x, y] = 5;

            for (int ix = -1; ix < 2; ix++)
            {
                for (int iy = -1; iy < 2; iy++)
                {
                    int xx = x + ix;
                    int yy = y + iy;

                    if(xx>=0 && xx<=9 && yy>=0 && yy<=9)
                    {
                        if (table[xx, yy] == 0)
                            table[xx, yy] = -1;
                    }
                }
            }

            if (x > 0 && table[x - 1, y] == 1) markSinkedFloodFill(x - 1, y);
            if (x < 9 && table[x + 1, y] == 1) markSinkedFloodFill(x + 1, y);
            if (y > 0 && table[x, y - 1] == 1) markSinkedFloodFill(x, y - 1);
            if (y < 9 && table[x, y + 1] == 1) markSinkedFloodFill(x, y + 1);


        }

        private Dictionary<string, string> hitted()
        {
            table[target.Item1, target.Item2] = 1;

            return new Dictionary<string, string>();
        }

        private Dictionary<string, string> missed()
        {
            table[target.Item1, target.Item2] = -1;

            return new Dictionary<string, string>();
        }

        private Dictionary<string, string> aimAndShoot()
        {
            List<Tuple<int, int, int>> candidates = new List<Tuple<int, int, int>>();

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if(table[x, y]==0)
                    {

                        int priority = 1;
                        if (x > 0 && table[x - 1, y] == 1) priority = 2;
                        if (x < 9 && table[x + 1, y] == 1) priority = 2;
                        if (y > 0 && table[x, y - 1] == 1) priority = 2;
                        if (y < 9 && table[x, y + 1] == 1) priority = 2;
                        candidates.Add(new Tuple<int, int, int>(x, y, priority));
                    }
                }
            }
            Random r = new Random();
            candidates = candidates.OrderByDescending(i => i.Item3).ToList();
            if(candidates.Count!=0)
            {
                List<Tuple<int, int, int>> vipCandidates = candidates.Where(i => i.Item3 == candidates[0].Item3).ToList();
                target = vipCandidates[r.Next(vipCandidates.Count)];
                return new Dictionary<string, string>() { { "action", "shoot" }, { "x", target.Item1.ToString() }, { "y", target.Item2.ToString() } };
            }

            return new Dictionary<string, string>() { {"action", "noTarget"}};
        }

        private Dictionary<string, string> buildShipsDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int iterator = 1;
            dict.Add("action", "ships");
            foreach (Ship ship in ships)
            {
                dict.Add("x"+iterator, ship._X.ToString());
                dict.Add("y" + iterator, ship._Y.ToString());
                dict.Add("length" + iterator, ship._length.ToString());
                dict.Add("direction" + iterator, ship._orientation.ToString());

                iterator++;
            }
            return dict;
        }
    }
}
