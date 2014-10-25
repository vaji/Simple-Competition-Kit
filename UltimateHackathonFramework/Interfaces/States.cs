using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Interfaces
{
    public static class Enums
    {
        public enum State { Ready, Standby, Failed, Lost, Victory }
        public enum ServerStateEnum { Listening, NotListening }
    }
}
