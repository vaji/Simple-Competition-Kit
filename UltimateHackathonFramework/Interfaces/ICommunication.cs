﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Interfaces
{
    public interface ICommunication: IDisposable
    {
        void StartListening(string IpAddress, int PortNumber);
        System.Net.Sockets.TcpClient GetConnectedClient();
        void StopListening();

        ServerStateEnum ServerState { get; set; }

        string IP { get; set; }

        string Port { get; set; }

        event Action StateChanged;
    }
}
