using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ShipsBot
{
    class Ship
    {

        public int _length=0;
        public int _X=-1;
        public int _Y=-1;
        public int _orientation=-1;

        public Ship()
        {

        }

        Tuple<int, int> getEndPoint()
        {
            int x2=0, y2=0;
            switch (_orientation)
            {
                case 0:
                    {
                        x2 = _X;
                        y2 = _Y - _length;
                        break;
                    }
                case 1:
                    {
                        x2 = _X + _length;
                        y2 = _Y;
                        break;
                    }
                case 2:
                    {
                        x2 = _X;
                        y2 = _Y + _length;
                        break;
                    }
                case 3:
                    {
                        x2 = _X - _length;
                        y2 = _Y;
                        break;
                    }

                default:
                    break;
            }
            return new Tuple<int, int>(x2, y2);
        }



        internal bool intersects(Ship _ship)
        {
            Tuple<int, int> p2 = getEndPoint();
            if (checkIfExceedsBoundaries(p2))
                return true;

            Tuple<int, int> p2a = _ship.getEndPoint();
            if(cross(_X, _Y, p2.Item1, p2.Item2, _ship._X, _ship._Y, p2a.Item1, p2a.Item2)) return true;

            if (cross(_X-1, _Y-1, p2.Item1-1, p2.Item2+1, _ship._X, _ship._Y, p2a.Item1, p2a.Item2)) return true;
            if (cross(_X-1, _Y-1, p2.Item1+1, p2.Item2-1, _ship._X, _ship._Y, p2a.Item1, p2a.Item2)) return true;
            if (cross(_X-1, _Y+1, p2.Item1+1, p2.Item2+1, _ship._X, _ship._Y, p2a.Item1, p2a.Item2)) return true;
            if (cross(_X+1, _Y-1, p2.Item1+1, p2.Item2+1, _ship._X, _ship._Y, p2a.Item1, p2a.Item2)) return true;
            return false;
        }

        private bool checkIfExceedsBoundaries(Tuple<int, int> p2)
        {
            
            if (p2.Item1 < 0 || p2.Item1 > 9 || p2.Item2 < 0 || p2.Item2 > 9)
                return true;
            else
                return false;
        }

        static bool cross(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
             float c, d, e, f, g, h, u1, u2;
             c=x3-x1;
             d=x3-x4;
             e=x2-x1;
             f=y3-y1;
             g=y3-y4;
             h=y2-y1;
             if(e*g-d*h!=0&&h*d-e*g!=0)
             {
                  u1=(e*f-c*h)/(e*g-d*h);
                  u2=(d*f-c*g)/(h*d-e*g);
             }
             else
             {
                  return false;
             }
             if(u1>=0&&u1<=1&&u2>=0&&u2<=1)
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
