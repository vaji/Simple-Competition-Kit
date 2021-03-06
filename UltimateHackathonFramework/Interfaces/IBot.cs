﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Interfaces
{
    public interface IBot
    {
        string ID { get; }
        string Name { get; }

        double Points { get; }
        void AddPoints(double points);
        void ClearPoints();
        TcpClient CommunicationChannel { get; set; }

        State CurrentState { get; set; } 

        Dictionary<string,string> Communicate(Dictionary<string, string> data);

        void RunBot();
        void KillBot();

        string Directory { get; }
    }
}
