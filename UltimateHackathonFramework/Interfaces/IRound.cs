using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UltimateHackathonFramework.Interfaces
{
    public interface IRound
    {
        UltimateHackathonFramework.Models.ConfigRound Config
        {
            get;
            set;
        }

        IResult Result
        {
            get;
        }


        IResult Go(IEnumerable<IBot> bots);
    }
}
