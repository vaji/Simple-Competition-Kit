using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Diagnostics;

namespace FakeBot
{
   
   public  class Program
    {
        static Communication communication = null;
        static TTTBot bot;
        static void Main(string[] args)
        {
        
            Console.WriteLine(args[0] + "  " + args[1]);
            communication = new Communication();
            communication.Connect(args[0], Convert.ToInt32(args[1]));
            if(communication.connected)
            {
                Console.WriteLine("connected");
               bot = new TTTBot();

                bot.Init();
                while (!bot.game_finished)
                {

                   
                    
                }
               


            }
            else
            {
                Console.WriteLine("Not connected");
            }
            
        }

         static void communication_ServerExceptionEvent(Exception exception)
        {

        }


        public static Dictionary<string, string> Communicate(string data)
        {
           // 
            string json = data;
            try
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            catch (Exception)
            {


            }
            return null;
        }
       public static void communication_ServerWriteEvent(string message)
        {
            Console.WriteLine("Server said: " + message);
            Dictionary<string,string> BotRespond =  bot.Play(Communicate(message));

            string json = new JavaScriptSerializer().Serialize(BotRespond.ToDictionary(item => item.Key.ToString(), item => item.Value.ToString()));

            communication.SendString(json);
            
        }
    }
}
