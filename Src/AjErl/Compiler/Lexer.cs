namespace AjErl.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class Lexer
    {
        private TextReader reader;

        public Lexer(string text)
            : this(new StringReader(text))
        {
        }

        public Lexer(TextReader reader)
        {
            this.reader = reader;
        }

        public Token NextToken()
        {
            int ich = this.NextChar();

            string name = string.Empty;

            while (ich != -1)
            {
                char ch = (char)ich;
                name += ch;
                ich = this.NextChar();
            }

            return new Token(name, TokenType.Atom);
        }

        private int NextChar()
        {
            return this.reader.Read();
        }
    }
}
