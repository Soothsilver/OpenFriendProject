using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Conversation.SCL
{
    class SclLexicalLine
    {
        public SclLineKind Kind { get; }
        public string PureText { get; }
        public string[] Arguments { get; }
        public string Command { get; }
        public int IndentCount { get; }

        public static SclLexicalLine Parse(string fullLine)
        {
            string trimmed = fullLine.Trim();
            if (String.IsNullOrEmpty(trimmed)) return null;
            int lastTab = fullLine.LastIndexOf('\t');
            int indentCount = lastTab + 1;
            if (trimmed[0] == '-')
            {
                return new SCL.SclLexicalLine(SclLineKind.MenuOption, indentCount, trimmed.Substring(1), null, null);
            }
            else if (trimmed[0] == '/')
            {
                string[] split = trimmed.Split(new[] {  ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return new SCL.SclLexicalLine(SclLineKind.Command, indentCount, null, split[0], split.Skip(1).ToArray());
            }
            else
            {
                return new SCL.SclLexicalLine(SclLineKind.SimpleLine, indentCount, trimmed, null, null);
            }
        }

        private SclLexicalLine(SclLineKind kind, int indentCount, string pureText, string command, string[] arguments)
        {
            Kind = kind;
            IndentCount = indentCount;
            PureText = pureText;
            Command = command;
            Arguments = arguments;
        }
    }
    enum SclLineKind
    {
        SimpleLine,
        MenuOption,
        Command
    }
}
