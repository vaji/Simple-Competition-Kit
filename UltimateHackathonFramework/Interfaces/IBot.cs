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
        void addPoints(double points);
        TcpClient CommunicationChannel { get; set; }

        string CurrentStatus { get; set; } 

        Dictionary<string,string> Communicate(Dictionary<string, string> data);

        void RunBot();
        void KillBot();
    }
}
