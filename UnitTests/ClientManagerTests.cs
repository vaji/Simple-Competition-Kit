using System;
using NUnit.Framework;
using UltimateHackathonFramework.Interfaces;
using UltimateHackathonFramework.Models;

namespace UnitTests
{
    [TestFixture]
    public class ClientManagerTests
    {
        [Test]
        public void ShouldDetectBots()
        {
            string currentDirName = System.IO.Directory.GetCurrentDirectory();
            System.IO.Directory.CreateDirectory(currentDirName+@"\Bots");
            System.IO.Directory.CreateDirectory(currentDirName+@"\Bots\Bot_testowy");
            System.IO.File.Create(currentDirName+@"\Bots\Bot_testowy\Main.exe");
            IClientManager clientManager= new ClientManager();
            clientManager.ScanForClients();
            Assert.AreEqual(1,clientManager.Clients.Count);
            Assert.AreEqual("Bot_testowy",clientManager.Clients[0].Name);
            
 




            Assert.Fail();
            

        }
    }
}
