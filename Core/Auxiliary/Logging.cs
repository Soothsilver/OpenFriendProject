using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Logging
    {
        public static void Log(string lines)
        {
            StreamWriter sw = new StreamWriter("log.txt", true);
            sw.WriteLine(lines);
            sw.Close();
        }
    }
}
