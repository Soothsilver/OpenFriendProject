using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation.Nodes;
using Core.Conversation.SCL;

namespace Core.Conversation
{
    public class DialogueLoader
    {
        public ConversationNode LoadFromFile(string filename)
        {

            // Load text into unparsed SclLine
            string[] lines = System.IO.File.ReadAllLines(filename);
            List<SclLexicalLine> sclLines = new List<SclLexicalLine>();
            foreach(var line in lines)
            {
                var sclLine = SCL.SclLexicalLine.Parse(line);
                if (sclLine != null)
                {
                    sclLines.Add(sclLine);
                }
            }


            int currentLine = 0;

            SclParser p = new SclParser();
            SclConversation c = p.ParseConversation(sclLines, 0, ref currentLine);

            return c.ToConversationNode(c);
        }
    }
}
