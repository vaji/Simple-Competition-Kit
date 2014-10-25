﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Interfaces
{
    public interface IClientManager
    {
        IList<IBot> Clients
        {
            get;
            set;
        }
        
        void ScanForClients(string name);
        
    }
}
