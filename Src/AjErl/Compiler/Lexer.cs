namespace AjErl.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

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

            char ch = (char)ich;

            if (operators.Contains(ch))
                return new Token(ch.ToString(), TokenType.Operator);
            if (separators.Contains(ch))
                return new Token(ch.ToString(), TokenType.Separator);

            if (char.IsDigit(ch))
                return this.NextInteger(ch);

            if (ch == '"')
                return this.NextString();

            if (IsNameChar(ch))
                return this.NextName(ch);

            throw new ParserException(string.Format("unexpected '{0}'", ch));
        }

        private static bool IsNameChar(char ch)
        {
            if (char.IsLetterOrDigit(ch) || ch == '_' || ch == '@')
                return true;

            return false;
        }

        private Token NextName(char ch)
        {
            string value = ch.ToString();
            TokenType type = char.IsUpper(ch) || ch == '_' ? TokenType.Variable : TokenType.Atom;
            int ich;

            for (ich = this.NextChar(); ich != -1 && IsNameChar((char)ich); ich = this.NextChar())
                value += (char)ich;

            this.PushChar(ich);

            return new Token(value, type);
        }

        private Token NextInteger(char ch)
        {
            string value = ch.ToString();
            int ich;

            for (ich = this.NextChar(); ich != -1 && char.IsDigit((char)ich); ich = this.NextChar())
                value += (char)ich;

            if (ich != -1 && (char)ich == '.')
                return this.NextReal(value);

            this.PushChar(ich);

            return new Token(value, TokenType.Integer);
        }

        private Token NextReal(string integer)
        {
            string value = integer + ".";

            int ich;

            for (ich = this.NextChar(); ich != -1 && char.IsDigit((char)ich); ich = this.NextChar())
                value += (char)ich;

            this.PushChar(ich);

            if (value.EndsWith("."))
            {
                this.PushChar('.');
                return new Token(value.Substring(0, value.Length - 1), TokenType.Integer);
            }

            return new Token(value, TokenType.Real);
        }

        private Token NextString()
        {
            string value = string.Empty;
            int ich;

            for (ich = this.NextChar(); ich != -1 && (char)ich != '"'; ich = this.NextChar())
                value += (char)ich;

            if (ich == -1)
                throw new ParserException("unclosed string");

            return new Token(value, TokenType.String);
        }

        private int NextChar()
        {
            int ich = this.NextSimpleChar();

            if (ich >= 0 && (char)ich == '%')
                for (ich = this.NextSimpleChar(); ich != -1 && (char)ich != '\n';)
                    ich = this.NextSimpleChar();

            return ich;
        }

        private int NextSimpleChar()
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
    }
}

