using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Conversation.SCL;

namespace Core.Conversation
{
    class SclParser
    {
        public SclConversation ParseConversation(List<SclLexicalLine> lines, int atIndent, ref int currentLine)
        {
            SclConversation statementSequence = new SclConversation();
            while (currentLine < lines.Count)
            {
                if (lines[currentLine].IndentCount < atIndent) break;
                ParseConversationLine(lines, atIndent, statementSequence, ref currentLine);
            }
            return statementSequence;
        }

        private void ParseConversationLine(List<SclLexicalLine> lines, int atIndent, SclStatementSequence statementSequence,
            ref int currentLine)
        {
            SclLexicalLine line = lines[currentLine];
            if (currentLine < lines.Count - 1 && lines[currentLine+1].IndentCount > atIndent)
            {
                ParseMenu(lines, line.IndentCount, statementSequence, ref currentLine);
            }
            else if (line.Kind == SclLineKind.Command)
            {
                statementSequence.Statements.Add(new SclCommandStatement(line));
                currentLine++;
            }
            else if (line.Kind == SclLineKind.SimpleLine)
            {
                statementSequence.Statements.Add(new SclSimpleLineStatement(line));
                currentLine++;
            }
            else
            {
                throw new Exception("Bad parse.");
            }
        }

        private void ParseMenu(List<SclLexicalLine> lines, int indentCount, SclStatementSequence statementSequence,
            ref int currentLine)
        {
            SclMenu menu = new Conversation.SclMenu(lines[currentLine].PureText);
            currentLine++;
            indentCount = lines[currentLine].IndentCount;
            while (lines[currentLine].IndentCount >= indentCount)
            {
                var line = lines[currentLine];
                if (line.IndentCount == indentCount && line.Kind == SclLineKind.MenuOption)
                {
                    string caption = line.PureText;
                    currentLine++;
                    SclStatementSequence theAnswer = ParseConversation(lines, indentCount + 1, ref currentLine);
                    menu.Options.Add(new SclMenuOption()
                    {
                        Caption = caption,
                        Contents = theAnswer
                    });
                }
                else
                {
                    throw new Exception("Parse error");
                }
            }
            statementSequence.Statements.Add(menu);
        }
    }
}
