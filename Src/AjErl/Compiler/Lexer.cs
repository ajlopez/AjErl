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
            int ich = this.NextCharSkippingWhiteSpaces();

            if (ich == -1)
                return null;

            string name = string.Empty;

            while (ich != -1 && !char.IsWhiteSpace((char) ich))
            {
                char ch = (char)ich;
                name += ch;
                ich = this.NextChar();
            }

            if (char.IsUpper(name[0]))
                return new Token(name, TokenType.Variable);

            return new Token(name, TokenType.Atom);
        }

        private int NextChar()
        {
            return this.reader.Read();
        }

        private int NextCharSkippingWhiteSpaces()
        {
            int ich = this.NextChar();

            while (ich != -1 && char.IsWhiteSpace((char)ich))
                ich = this.NextChar();

            return ich;
        }
    }
}

