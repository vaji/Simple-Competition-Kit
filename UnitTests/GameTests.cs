using System;
using NUnit.Framework;
using System.Collections.Generic;
using UltimateHackathonFramework.Interfaces;
using UltimateHackathonFramework.Games;

namespace UnitTests
{
    [TestFixture]

    public class WarGameBot : IBot
    { 
        string testString = "hello";
        public string ID
        {
	        get;
            set;
        }

        public string Name
        {
	        get;
            set;
        }

        public System.Collections.Generic.Dictionary<string,string> Communicate(System.Collections.Generic.Dictionary<string,string> data)
        {
            Dictionary<string, string> resultDictionary = new Dictionary<string, string>();
           
            if(data.ContainsKey("hello"))
            {
                 resultDictionary.Add("hello",data["hello"]);
            }
            else if (data.ContainsKey("win"))
            {
                resultDictionary.Add("win", data["win"]);
            }

            return resultDictionary;
        }

        public void RunBot()
        {
 	        throw new NotImplementedException();
        }

        public void KillBot()
        {
 	        throw new NotImplementedException();
        }

        public WarGameBot (string id, string name)
	    {
            ID = id;
            Name = id;
       
	    }


        public System.Net.Sockets.TcpClient CommunicationChannel
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void RunBot(ICommunication server)
        {
            throw new NotImplementedException();
        }


        public string CurrentStatus
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public double Points
        {
            get { throw new NotImplementedException(); }
        }

        public void AddPoints(double points)
        {
            throw new NotImplementedException();
        }


        public Enums.State CurrentState
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
    public class GameTests
    {
        [Test]
        public void TestMethod1()
        {
            Assert.Fail();

        }

        [Test]
        public void TestWarGame()
        { 
            List<IBot> testBots = new List<IBot>();
            WarGame testGame = new WarGame();

            for (var i = 0; i < 5; i++)
            {
                testBots.Add(new WarGameBot(i+"", i+""));
            }

            testGame.Start(testBots);

            if (testGame.jest_ok && testGame.wygrany_index != -1)
            {
                Assert.Pass();
            }
            

        }
    }
}
