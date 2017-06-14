using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace SimpleConversationLanguage
{
    public static class Exs
    {
        public static string ReplaceAll(this string what, string regex, string replacement)
        {
            Regex r = new Regex(regex);
            return r.Replace(what, replacement);
        }
    }
    public class SclParser
    {
        public void a(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            var lexer = new SimpleConversationLanguageLexer(inputStream);
            var tokens = new CommonTokenStream(lexer);
            Console.WriteLine("Tokens:");
            tokens.Fill();
            foreach (var token in tokens.GetTokens())
            {
                Console.Write("'" + token.Text.Replace("\r", "").Replace("\n", "[NL]") + "' ");
            }
            var parser = new SimpleConversationLanguageParser(tokens);
            var listener = new SclListener();
            parser.AddParseListener(listener);
            parser.conversation();
        }
    }
    public class SclListener : SimpleConversationLanguageBaseListener
    {
        public override void ExitConversation([NotNull] SimpleConversationLanguageParser.ConversationContext context)
        {
            base.ExitConversation(context);
        }

    }
}
