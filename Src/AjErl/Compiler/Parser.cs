namespace AjErl.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
            IExpression expression = this.ParseSimpleExpression();

            Token token = this.NextToken();

            if (expression == null && token == null)
                return null;

            if (token != null && token.Type == TokenType.Operator && token.Value == "=")
            {
                expression = new MatchExpression(expression, this.ParseSimpleExpression());
                this.ParsePoint();
                return expression;
            }
            else
                this.PushToken(token);

            this.ParsePoint();

            return expression;
        }

        private IExpression ParseSimpleExpression()
        {
            Token token = this.NextToken();
            IExpression expression = null;

            if (token == null)
                return null;

            if (token.Type == TokenType.Variable)
                expression = new VariableExpression(new Variable(token.Value));
            else if (token.Type == TokenType.Atom)
                expression = new AtomExpression(new Atom(token.Value));
            else if (token.Type == TokenType.Integer)
                expression = new ConstantExpression(int.Parse(token.Value, CultureInfo.InvariantCulture));
            else if (token.Type == TokenType.String)
                expression = new ConstantExpression(token.Value);
            else if (token.Type == TokenType.Separator && token.Value == "{")
            {
                var expressions = this.ParseExpressionList();
                this.ParseToken(TokenType.Separator, "}");
                expression = new TupleExpression(expressions);
            }
            else
                this.PushToken(token);

            return expression;
        }

        private IList<IExpression> ParseExpressionList()
        {
            List<IExpression> expressions = new List<IExpression>();

            for (IExpression expr = this.ParseSimpleExpression(); expr != null; expr = this.ParseSimpleExpression())
            {
                expressions.Add(expr);

                Token token = this.NextToken();

                if (token != null && token.Type == TokenType.Separator && token.Value == ",")
                    continue;

                if (token != null)
                    this.PushToken(token);

                break;
            }

            return expressions;
        }

        private Token NextToken()
        {
            return this.lexer.NextToken();
        }

        private void PushToken(Token token)
        {
            this.lexer.PushToken(token);
        }

        private void ParseToken(TokenType type, string value)
        {
            Token token = this.NextToken();

            if (token == null)
                throw new ParserException(string.Format("Expected '{0}'", value));

            if (token.Type != type || token.Value != value)
                throw new ParserException(string.Format("Unexpected '{0}'", token.Value));
        }

        private void ParsePoint()
        {
            Token token = this.NextToken();

            if (token == null)
                throw new ParserException("Expected '.'");

            if (token.Type != TokenType.Separator || token.Value != ".")
                throw new ParserException(string.Format("Unexpected '{0}'", token.Value));
        }
    }
}
