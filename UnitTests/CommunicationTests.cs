using System;
using NUnit.Framework;
using UltimateHackathonFramework.Interfaces;
using UltimateHackathonFramework.Models;
using System.Net.Sockets;
using System.IO;

namespace UnitTests
{
    [TestFixture]
    public class CommunicationTests
    {
        [Test]
        public void TestMethod1()
        {
            ICommunication communication = new Communication();
            //communication
            communication.StartListening("127.0.0.1", 12345);
            
            IBot sampleBot = new Bot(communication, "testBot",  Directory.GetCurrentDirectory()+@"\..\..\..\Tests\FakeBot.exe");
            sampleBot.RunBot();
            string REQUEST = "gdzie jest groszek?";
            string RESPONSE = "tutaj";
            System.Collections.Generic.Dictionary<string, string> response= sampleBot.Communicate(new System.Collections.Generic.Dictionary<string,string>(){{REQUEST, RESPONSE}});
            sampleBot.KillBot();
            Assert.AreEqual(RESPONSE, response[REQUEST]); 
        }
    }
}
