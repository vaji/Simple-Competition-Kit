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

                }while(contains(ships[i], ships.GetRange(0, i-1)));
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

            if (dict.ContainsKey("aim"))
            {
                return aimAndShoot();
            }


            return new Dictionary<string, string>();
        }

        private Dictionary<string, string> aimAndShoot()
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, string> buildShipsDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            int iterator = 1;
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
