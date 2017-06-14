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
            Dictionary<string, ConversationNode> labeledNodes = new Dictionary<string, Core.ConversationNode>();

            string[] lines = System.IO.File.ReadAllLines(filename);
            List<SclLine> sclLines = new List<SclLine>();
            foreach(var line in lines)
            {
                var sclLine = SCL.SclLine.Parse(line);
                if (sclLine != null)
                {
                    sclLines.Add(sclLine);
                }
            }


            int currentLine = 0;
            ConversationNode firstNode = new DoNothingNode();
            ConversationNode lastNode = firstNode;
            while (currentLine < lines.Length)
            {
                ParseLine(sclLines, 0, ref lastNode, ref currentLine);
            }

            return firstNode;
        }

        private void ParseLine(List<SclLine> lines, int atIndent, ref ConversationNode lastNode, ref int currentLine)
        {
            SclLine line = lines[currentLine];

            // Menu
            if (line.IndentCount > atIndent)
            {
                //ParseMenu()
            }


            // Basic line 
            ConversationNode node = new Line(line.PureText);
            lastNode.FollowingNode = node;
            lastNode = node;

            // Continue
            currentLine++;
        }
    }
}
