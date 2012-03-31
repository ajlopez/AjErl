namespace AjErl.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class Lexer
    {
        private static string operators = "=";
        private static string separators = ".";
        private TextReader reader;
        private Stack<int> chars = new Stack<int>();

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

            if (operators.Contains((char)ich))
                return new Token(((char)ich).ToString(), TokenType.Operator);
            if (separators.Contains((char)ich))
                return new Token(((char)ich).ToString(), TokenType.Separator);

            string name = string.Empty;

            while (ich != -1 && IsNameChar((char) ich))
            {
                char ch = (char)ich;
                name += ch;
                ich = this.NextChar();
            }

            if (ich != -1)
                this.PushChar(ich);

            if (char.IsUpper(name[0]) || name[0] == '_')
                return new Token(name, TokenType.Variable);

            return new Token(name, TokenType.Atom);
        }

        private int NextChar()
        {
            if (this.chars.Count > 0)
                return this.chars.Pop();

            return this.reader.Read();
        }

        private int NextCharSkippingWhiteSpaces()
        {
            int ich = this.NextChar();

            while (ich != -1 && char.IsWhiteSpace((char)ich))
                ich = this.NextChar();

            return ich;
        }

        private void PushChar(int ich)
        {
            this.chars.Push(ich);
        }

        private static bool IsNameChar(char ch)
        {
            if (char.IsLetterOrDigit(ch) || ch == '_')
                return true;

            return false;
        }
    }
}

