using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;

namespace OpenFriendProjectConsoleResponder
{
    class Program
    {
        static void Main(string[] args)
        {
            Overseer overseer = new Core.Overseer();
            overseer.Speaking.DebugMessage += Speaking_DebugMessage;
            overseer.Initialize();
            MetaCommunicator meta = new MetaCommunicator(overseer);
            Task t = meta.StartLoop();
            Thread.Sleep(500);
                Console.WriteLine("Initing command loop...");
                while (true)
                {
                    Console.WriteLine("> ");
                    string command = Console.ReadLine().ToLower().Trim();
                    switch (command)
                    {
                        case "quit":
                        case "exit":
                        case "q":
                        case "x":
                            return;
                        case "update-settings":
                            overseer.Settings.UpdateMessengerSettings();
                            break;
                    }
                }

        }

        private static void Speaking_DebugMessage(string obj)
        {
            Console.WriteLine("[" + obj);
        }
    }
}
