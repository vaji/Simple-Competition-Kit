﻿using System;
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
    }

    class TTTBot
    {
        field[] pola;
        public bool game_finished = false;
        public string my_sign = "";
        public void Init()
        {
            pola = new field[9];
          

            for (var i = 0; i < 9; i++)
            {
                pola[i].taken = false;
                pola[i].nr = i;
                pola[i].sign = "";
            }
        }


        public Dictionary<string,string> Play(Dictionary<string,string> data)
        {
            if (data.ContainsKey("hello"))
            {
                my_sign = data["hello"];
                return data;
            }
            else if (data.ContainsKey("move"))
            {
                if (data["move"] == "")
                {
                    bool picked_field = false;
                    int field_picked = -1;
                    Random rnd = new Random();
                    while (!picked_field)
                    {
                        int p = rnd.Next(0, 9);
                        if (!pola[p].taken) { field_picked = p; break; }
                    }
                    data["move"] = field_picked + "";
                    return data;
                }
            }
            else if (data.ContainsKey("X"))
            {
                pola[int.Parse(data["X"])].taken = true;
                pola[int.Parse(data["X"])].sign = "X";
                return data;
            }
            else if (data.ContainsKey("O"))
            {
                pola[int.Parse(data["O"])].taken = true;
                pola[int.Parse(data["O"])].sign = "O";
                return data;
            }
            else if (data.ContainsKey("win"))
            {
                return data;
            }

            return data;
        }


    }
}