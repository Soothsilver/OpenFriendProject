using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace WinformsFriend
{
    class Server
    {
        public static Overseer Overseer;

        public static void Init()
        {
            Overseer = new Overseer();
        }
    }
}
