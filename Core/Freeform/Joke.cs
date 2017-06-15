using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Freeform
{
    class Joke
    {
        public static List<string> Jokes = new List<string>();
        private static bool Loaded;
        public static void Load()
        {
            if (Loaded) return;
            Loaded = true;
            string allText = System.IO.File.ReadAllText(@"Data/oneliners.txt").Replace("\r","");
            string[] jokes = allText.Split(new string[] {"\n\n"}, StringSplitOptions.RemoveEmptyEntries);
            foreach(var joke in jokes)
            {
                string jk = joke.Trim().Replace("\r", "").Replace('\n', ' ');
                Jokes.Add(jk);
            }
        }
    }
}
