using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltimateHackathonFramework.Interfaces;
using UltimateHackathonFramework.Models;
using UltimateHackathonFramework.Games;
using NUnit.Framework;
using System.Net.Sockets;


namespace UnitTests
{
    [TestFixture]

    public class ShipsTest
    {
        [Test]
        public void Test()
        {
            //ICommunication comm = new 
            ICommunication communication = new Communication();
            communication.StartListening("127.0.0.1", 12345);
            IBot sampleBot1 = new Bot(communication, "testBot", System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\Tests\ShipsBot.exe");
            sampleBot1.RunBot();
            IBot sampleBot2 = new Bot(communication, "testBot", System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\Tests\ShipsBot.exe");
            sampleBot2.RunBot();
            Game statki = new Ships();
            IList<IBot> list = new List<IBot>();
            list.Add(sampleBot1);
            list.Add(sampleBot2);
            statki.Go(list);
            Assert.Fail();
        }


    }
}
