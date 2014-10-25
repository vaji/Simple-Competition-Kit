using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;
using UltimateHackathonFramework.Models;
using NUnit.Framework;
using System.Net.Sockets;


namespace UnitTests
{
    [TestFixture]

    class ShipsTest
    {

        //ICommunication comm = new 
        ICommunication communication = new Communication();

        communication.StartListening("127.0.0.1", 12345);
        IBot sampleBot = new Bot(communication, "testBot",  System.IO.Directory.GetCurrentDirectory()+@"\..\..\..\Tests\ShipsBot.exe");
        sampleBot.RunBot();
    }
}
