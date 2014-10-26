using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBot
{
    struct field
    {
        public int nr;
        public bool taken;
        public string sign;
        public int priority;
    }

    class TTTBot
    {
        field[] pola;
        int[,] linie;
        public bool game_finished = false;
        public string my_sign = "";
        public Communication comm;
        public void Init()
        {
            pola = new field[9];
            linie = new int[8, 3];
            linie[0, 0] = 0; linie[0, 1] = 1; linie[0, 2] = 2;
            linie[1, 0] = 3; linie[1, 1] = 4; linie[1, 2] = 5;
            linie[2, 0] = 6; linie[2, 1] = 7; linie[2, 2] = 8;
            linie[3, 0] = 0; linie[3, 1] = 3; linie[3, 2] = 6;
            linie[4, 0] = 1; linie[4, 1] = 4; linie[4, 2] = 7;
            linie[5, 0] = 2; linie[5, 1] = 5; linie[5, 2] = 8;
            linie[6, 0] = 0; linie[6, 1] = 4; linie[6, 2] = 8;
            linie[7, 0] = 2; linie[7, 1] = 4; linie[7, 2] = 6;

            for (var i = 0; i < 9; i++)
            {
                pola[i].taken = false;
                pola[i].nr = i;
                pola[i].sign = "";
                if (i == 4) pola[i].priority = 20;
                else pola[i].priority = 0;
            }
        }


        public Dictionary<string,string> Play(Dictionary<string,string> data)
        {
            if (data.ContainsKey("youAre"))
            {
                my_sign = data["youAre"];
                return data;
            }
            else if (data.ContainsKey("move"))
            {
                if (data["move"] == "")
                {
                    bool picked_field = false;
                    int field_picked = -1;
                    while (!picked_field)
                    {
                       
                        field_picked = PickField();
                        if(!pola[field_picked].taken)
                        { 
                           picked_field = true; break;
                        }
                        Console.Write(field_picked);
                    }
                    data["move"] = field_picked + "";
                    return data;
                }
                else 
                {
                    if (pola[int.Parse(data["move"])].taken!=true)
                    {
                        pola[int.Parse(data["move"])].taken = true;
                        if(my_sign=="X")
                        {

                            SetPriority(int.Parse(data["move"]), 3);
                            pola[int.Parse(data["move"])].sign = "O";
                        }
                        else
                        {
                            SetPriority(int.Parse(data["move"]), 3);
                            pola[int.Parse(data["move"])].sign = "X";
                        }
                    }
                    
                }
            }
            else if (data.ContainsKey("X"))
            {
                pola[int.Parse(data["X"])].taken = true;
                pola[int.Parse(data["X"])].sign = "X";
                SetPriority(int.Parse(data["X"]), 1);
                return data;
            }
            else if (data.ContainsKey("O"))
            {
                pola[int.Parse(data["O"])].taken = true;
                pola[int.Parse(data["O"])].sign = "O";
                SetPriority(int.Parse(data["X"]), 1);
                return data;
            }
            else if (data.ContainsKey("win"))
            {
                return data;
            }
            else if(data.ContainsKey("tie"))
            {
                return data;
            }
            return data;
        }

        public int PickField()
        {
            int highest = 0;
            field[] picked_fields = new field[9];
            int picked_int = -1;
           // int picked_count = 0;
            for (var i = 0; i < 9; i++)
            {
               if (pola[i].priority >= highest && !pola[i].taken) highest = pola[i].priority;
            }

            for (int i = 0; i < 9; i++)
            {
                if (pola[i].priority == highest && !pola[i].taken)
                {
                    picked_int++;
                    picked_fields[picked_int] = pola[i];
                    
                }
            }

            if (picked_int >= 0)
            {
                int p = RandomGenerator.random.Next(0, picked_int);
                return picked_fields[p].nr;
            }
            else
            {
                return RandomGenerator.random.Next(0, 8);
            }
            /*
            picked_fields[picked_int] = pola[i];
            highest = pola[i].priority;
            picked_int++;
            picked_count++;

            if (picked_count == 0)
            {
                picked_count = 9;
            }
            picked_int = 0;
            int p = RandomGenerator.random.Next(0, picked_count-1);
            return p;
             * */
        }
        public void SetPriority(int index, int prior)
        {
            if (index == 0) {
                pola[1].priority += prior;
                pola[3].priority += prior;
                pola[4].priority += prior;

                if (CheckLineForTwo(0, 1)) pola[1].priority += 4; pola[2].priority += 4;
                if (CheckLineForTwo(0, 3)) pola[3].priority += 4; pola[6].priority += 4;
                if (CheckLineForTwo(0, 4)) pola[4].priority += 4; pola[8].priority += 4;
            }
            else if (index == 1)
            {
                pola[0].priority += prior;
                pola[2].priority += prior;
                pola[4].priority += prior;

                if (CheckLineForTwo(1, 0)) pola[0].priority += 4; pola[2].priority += 4;
                if (CheckLineForTwo(1, 4)) pola[4].priority += 4; pola[7].priority += 4;
            }
            else if (index == 2)
            {
                pola[1].priority += prior;
                pola[4].priority += prior;
                pola[5].priority += prior;

                if (CheckLineForTwo(2, 1)) pola[1].priority += 4; pola[0].priority += 4;
                if (CheckLineForTwo(2, 4)) pola[4].priority += 4; pola[6].priority += 4;
                if (CheckLineForTwo(2, 5)) pola[5].priority += 4; pola[8].priority += 4;
            }
            else if (index == 3)
            {
                pola[0].priority += prior;
                pola[4].priority += prior;
                pola[6].priority += prior;

                if (CheckLineForTwo(3, 0)) pola[0].priority += 4; pola[6].priority += 4;
                if (CheckLineForTwo(3, 4)) pola[4].priority += 4; pola[5].priority += 4;
          
            }
            else if (index == 4)
            {
                pola[0].priority += prior;
                pola[1].priority += prior;
                pola[2].priority += prior;
                pola[3].priority += prior;
                pola[5].priority += prior;
                pola[6].priority += prior;
                pola[7].priority += prior;
                pola[8].priority += prior;

                if (CheckLineForTwo(0, 8)) pola[0].priority += 4; pola[8].priority += 4;
                if (CheckLineForTwo(2, 6)) pola[2].priority += 4; pola[6].priority += 4;
                if (CheckLineForTwo(3, 5)) pola[3].priority += 4; pola[5].priority += 4;
                if (CheckLineForTwo(1, 7)) pola[1].priority += 4; pola[7].priority += 4;
            }
            else if (index == 5)
            {
                pola[2].priority += prior;
                pola[4].priority += prior;
                pola[8].priority += prior;

                if (CheckLineForTwo(5, 2)) pola[2].priority += 4; pola[8].priority += 4;
                if (CheckLineForTwo(5, 4)) pola[4].priority += 4; pola[3].priority += 4;
              
            }
            else if (index == 6)
            {
                pola[3].priority += prior;
                pola[4].priority += prior;
                pola[7].priority += prior;

                if (CheckLineForTwo(6, 0)) pola[3].priority += 4; pola[0].priority += 4;
                if (CheckLineForTwo(6, 4)) pola[4].priority += 4; pola[2].priority += 4;
                if (CheckLineForTwo(6, 7)) pola[7].priority += 4; pola[8].priority += 4;
            }
            else if (index == 7)
            {
                pola[4].priority += prior;
                pola[6].priority += prior;
                pola[8].priority += prior;

                if (CheckLineForTwo(7, 4)) pola[4].priority += 4; pola[1].priority += 4;
                if (CheckLineForTwo(7, 6)) pola[6].priority += 4; pola[8].priority += 4;
              
            }
            else if (index == 8)
            {
                pola[7].priority += prior;
                pola[6].priority += prior;
                pola[4].priority += prior;

                if (CheckLineForTwo(8, 4)) pola[4].priority += 4; pola[0].priority += 4;
                if (CheckLineForTwo(8, 5)) pola[5].priority += 4; pola[2].priority += 4;
                if (CheckLineForTwo(8, 7)) pola[7].priority += 4; pola[6].priority += 4;
            }

        }

        public bool CheckLineForTwo(int pole1, int pole2)
        {
            int linia = -1;
            bool linia_ok = false;
            bool match_1 = false;
            int check_done = -1;

            for (var i = 0; i < 8; i++)
            {
                match_1 = false;
                check_done = -1;
                linia_ok = false;
                for (var s = 0; s < 3; s++)
                {
                    if (linie[i, s] == pole1 || linie[i, s] == pole2) { match_1 = true; check_done = s; }
                }
                if(match_1)
                {
                   for (var s = 0; s < 3; s++)
                   {
                       if (s != check_done)
                       {
                           if (linie[i, s] == pole1 || linie[i, s] == pole2) { linia_ok = true; linia = i; break;  }
                       }
                   }
                }
             
            }
            if (linia_ok)
            {
                int counter = 0;
                for (var i = 0; i < 3; i++)
                {
                    if (pola[linie[linia, i]].sign == my_sign && pola[linie[linia, i]].taken) counter++;
                }
                if (counter >= 2)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }


    }
}
