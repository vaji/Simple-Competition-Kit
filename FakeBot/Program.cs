using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FakeBot
{
    class Program
    {
        static Communication communication = null;
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]+"  "+args[1]);
            communication = new Communication();
            communication.Connect(args[0], Convert.ToInt32(args[1]));
            if(communication.connected)
            {
                Console.WriteLine("connected");
            }
            else
            {
                Console.WriteLine("Not connected");
            }
            communication.ServerWriteEvent += communication_ServerWriteEvent;
            communication.ServerExceptionEvent += communication_ServerExceptionEvent;

             //JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            Console.ReadLine();
            communication.disconnect();

        }

        static void communication_ServerExceptionEvent(Exception exception)
        {

        }

        static void communication_ServerWriteEvent(string message)
        {
            Console.WriteLine("Server said: " + message);
            communication.SendString(message);
            
        }
    }
}
