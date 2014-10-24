using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework.Interfaces
{
    public interface IRound
    {
        IRoundResult Go(IList<IBot> bots);
    }
}
