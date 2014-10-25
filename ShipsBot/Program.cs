using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ShipsBot
{
    class Program
    {
        static Communication communication = null;
        static ShipBoat bot;
        static void Main(string[] args)
        {
            Console.WriteLine(args[0] + "  " + args[1]);
            communication = new Communication();
            communication.Connect(args[0], Convert.ToInt32(args[1]));
            if (communication.connected)
            {
                Console.WriteLine("connected");
                bot = new ShipBoat();
                bot.comm = communication;
                bot.Init();
            }
            else
            {
                Console.WriteLine("Not connected");
            }

            communication.ServerWriteEvent += communication_ServerWriteEvent;
            communication.ServerExceptionEvent += communication_ServerExceptionEvent;
        }

        private static void communication_ServerExceptionEvent(Exception exception)
        {
            //throw new NotImplementedException();
        }

        private static void communication_ServerWriteEvent(string message)
        {
            Console.WriteLine("Server said: " + message);
            Dictionary<string, string> dict = null;
            try
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);
            }
            catch (Exception)
            {
                return;
            }
            Dictionary<string, string> response = bot.consume(dict);

            string json = new JavaScriptSerializer().Serialize(response.ToDictionary(item => item.Key.ToString(), item => item.Value.ToString()));
            communication.SendString(json);
        }
    }
}
