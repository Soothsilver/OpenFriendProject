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
            Console.WriteLine("Started.");
            while (true)
            {
                Thread.Yield();
            }
        }
    }
}
