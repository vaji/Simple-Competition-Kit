using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateHackathonFramework
{
    interface IInterfejs
    {
    }
    class Conventions : IInterfejs
    {
        public int Property { get; set; }

        private int MyVar;

        public int MyProperty
        {
            get { return MyVar; }
            set { MyVar = value; }
        }

        private int _local;

        public int PublicMethod()
        { return 0; }
        private int PrivateMethod() { return 0; }

        public Conventions()
        {
            string variable;
            var localObject = new object();
        }
    }
}
