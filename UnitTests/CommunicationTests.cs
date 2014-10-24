using System;
using NUnit.Framework;
using UltimateHackathonFramework.Interfaces;
using UltimateHackathonFramework.Models;

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

            Assert.Fail();

        }
    }
}
