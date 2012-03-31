namespace AjErl.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using AjErl.Expressions;
    using AjErl.Language;

    public class Parser
    {
        private Lexer lexer;

        public Parser(string text)
            : this(new Lexer(text))
        {
        }

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public IExpression ParseExpression()
        {
            Token token = this.NextToken();
            IExpression expression = null;

            if (token == null)
                return null;

            if (token.Type == TokenType.Variable)
                expression = new VariableExpression(new Variable(token.Value));
            else if (token.Type == TokenType.Atom)
                expression = new AtomExpression(new Atom(token.Value));
            else
                throw new ParserException(string.Format("Unexpected '{0}'", token.Value));

            this.ParsePoint();

            return expression;
        }

        private Token NextToken()
        {
            return this.lexer.NextToken();
        }

        private void ParsePoint()
        {
            Token token = this.NextToken();

            if (token == null || token.Type != TokenType.Separator || token.Value != ".")
                throw new ParserException(string.Format("Unexpected '{0}'", token.Value));
        }
    }
}
