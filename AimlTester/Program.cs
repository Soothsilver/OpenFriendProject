using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AIMLbot;

namespace AimlTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot b = new Bot();
            User user = new User("Self.David", b);
            b.loadSettings();
            Console.WriteLine("Loading.");
            b.isAcceptingUserInput = false;
            b.loadAIMLFromFiles();
            b.isAcceptingUserInput = true;
            Console.WriteLine("Loaded.");
            while (true)
            {
                Request r = new Request(Console.ReadLine(), user, b);
                Result res = b.Chat(r);
                Console.WriteLine(res.Output);
            }
            /*
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("tarot.aiml");
            b.loadAIMLFromXML(xmlDocument, "tarot2.aiml");
            */
        }
    }
}
