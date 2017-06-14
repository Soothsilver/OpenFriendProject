grammar SimpleConversationLanguage;

tokens { INDENT, DEDENT }
@lexer::header {
	using System.Collections.Generic;
}
@lexer::members {
  // A queue where extra tokens are pushed on (see the NEWLINE lexer rule).
		  private List<IToken> tokens = new List<IToken>();
		  // The stack that keeps track of the indentation level.
		  private Stack<int> indents = new Stack<int>();
		  // The amount of opened braces, brackets and parenthesis.
		  private int opened = 0;
		  // The most recently produced token.
		  private IToken lastToken = null;
		  public override void Emit(IToken t) {
            base.Token = t;
		    tokens.Add(t);
		  }
	        
		  public override IToken NextToken() {
		    // Check if the end-of-file is ahead and there are still some DEDENTS expected.
		    if (_input.La(1) == TokenConstants.Eof && this.indents.Count != 0) {
		      // Remove any trailing EOF tokens from our buffer.
		      for (int i = tokens.Count - 1; i >= 0; i--) {
		        if (tokens[i].Type == TokenConstants.Eof) {
		          tokens.RemoveAt(i);
		        }
		      }

		      // First emit an extra line break that serves as the end of the statement.
		      this.Emit(commonToken(SimpleConversationLanguageParser.NEWLINE, "\n"));

		      // Now emit as much DEDENT tokens as needed.
		      while (indents.Count != 0) {
		        this.Emit(createDedent());
		          indents.Pop();
		      }

		      // Put the EOF back on the token stream.
		      this.Emit(commonToken(SimpleConversationLanguageParser.Eof, "<EOF>"));
		    }

		    IToken next = base.NextToken();

		    if (next.Channel == TokenConstants.DefaultChannel) {
		      // Keep track of the last token on the default channel.
		      this.lastToken = next;
		    }

		      IToken returned = tokens.Count == 0 ? next : tokens[0];
		      tokens.RemoveAt(0);
	            return returned;
		  }

		  private IToken createDedent() {
		    CommonToken dedent = commonToken(SimpleConversationLanguageParser.DEDENT, "");
		    dedent.Line = (this.lastToken.Line);
		    return dedent;
		  }

		  private CommonToken commonToken(int type, string text) {
		    int stop = this.CharIndex - 1;
		    int start = text == "" ? stop : stop - text.Length + 1;
		    return new CommonToken(this._tokenFactorySourcePair, type, TokenConstants.DefaultChannel, start, stop);
		  }

		  // Calculates the indentation of the provided spaces, taking the
		  // following rules into account:
		  //
		  // "Tabs are replaced (from left to right) by one to eight spaces
		  //  such that the total number of characters up to and including
		  //  the replacement is a multiple of eight [...]"
		  //
		  //  -- https://docs.python.org/3.1/reference/lexical_analysis.html#indentation
		  static int getIndentationCount(string spaces) {
		    int count = 0;
		    foreach (char ch in spaces) {
		      switch (ch) {
		        case '\t':
		          count += 8 - (count % 8);
		          break;
		        default:
		          // A normal space char.
		          count++;
	              break;
		      }
		    }

		    return count;
		  }

		  bool atStartOfInput() {
		    return base._tokenStartCharPositionInLine == 0 && base.Line == 1;
		  }
}

NL: ('\r'? '\n' ' '*);
 
conversation : singleline |
			   menu
			   ;

singleline : ANYTH+ NEWLINE;
menu: ANYTH INDENT ANYTH+ NEWLINE ANYTH+ NEWLINE ANYTH+ NEWLINE ANYTH+ NEWLINE ANYTH+ NEWLINE DEDENT;

anything : ANYTH;
ANYTH : [a-zA-Z0-9./${}]+;
fragment SPACES
 : [ \t]+
 ;
fragment LINE_JOINING
 : '\\' SPACES? ( '\r'? '\n' | '\r' | '\f')
 ;

SKIP_ : ( SPACES | LINE_JOINING ) -> skip ;

NEWLINE
 : ( {atStartOfInput()}?   SPACES
   | ( '\r'? '\n' | '\r' ) SPACES?
   )
   {
      string newLine = Text.ReplaceAll("[^\r\n]+", "");
				     string spaces = Text.ReplaceAll("[\r\n]+", "");
				     int next = _input.La(1);
				     if (opened > 0 || next == '\r' || next == '\n' || next == '#') {
				       // If we're inside a list or on a blank line, ignore all indents, 
				       // dedents and line breaks.
				       Skip();
				     }
				     else {
				       Emit(commonToken(NEWLINE, newLine));
				       int indent = getIndentationCount(spaces);
				         int previous = indents.Count == 0 ? 0 : indents.Peek();
				       if (indent == previous) {
		                            // skip indents of the same size as the present indent-size
				           Skip();
				       }
				       else if (indent > previous) {
				         indents.Push(indent);
				         Emit(commonToken(SimpleConversationLanguageParser.INDENT, spaces));
				       }
				       else {
				         // Possibly emit more than 1 DEDENT token.
				         while(indents.Count != 0 && indents.Peek() > indent) {
				           this.Emit(createDedent());
				           indents.Pop();
				         }
				       }
				     }
   }
 ;