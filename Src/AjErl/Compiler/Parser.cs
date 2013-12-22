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
        private static string[][] binaryoperators = new string[][] { new string[] { "+", "-" }, new string[] { "*", "/", "div", "rem" } };

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
                expression = new MatchExpression(expression, this.ParseBinaryExpression(0));
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
            return this.ParseBinaryExpression(0);
        }

        private IExpression ParseBinaryExpression(int level)
        {
            if (level >= binaryoperators.Length)
                return this.ParseTerm();

            IExpression expr = this.ParseBinaryExpression(level + 1);

            if (expr == null)
                return null;

            Token token;

            for (token = this.lexer.NextToken(); token != null && this.IsBinaryOperator(level, token); token = this.lexer.NextToken())
            {
                if (token.Value == "+")
                    expr = new AddExpression(expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "-")
                    expr = new SubtractExpression(expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "*")
                    expr = new MultiplyExpression(expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "/")
                    expr = new DivideExpression(expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "div")
                    expr = new DivExpression(expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "rem")
                    expr = new RemExpression(expr, this.ParseBinaryExpression(level + 1));
            }

            if (token != null)
                this.lexer.PushToken(token);

            return expr;
        }

        private IExpression ParseTerm()
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
            else if (token.Type == TokenType.Real)
                expression = new ConstantExpression(double.Parse(token.Value, CultureInfo.InvariantCulture));
            else if (token.Type == TokenType.String)
                expression = new ConstantExpression(token.Value);
            else if (token.Type == TokenType.Separator && token.Value == "(")
            {
                expression = this.ParseSimpleExpression();
                this.ParseToken(TokenType.Separator, ")");
            }
            else if (token.Type == TokenType.Separator && token.Value == "{")
            {
                var expressions = this.ParseExpressionList();
                this.ParseToken(TokenType.Separator, "}");
                expression = new TupleExpression(expressions);
            }
            else if (token.Type == TokenType.Separator && token.Value == "[")
            {
                var expressions = this.ParseExpressionList();
                IExpression tailexpression = null;

                if (this.TryParseToken(TokenType.Separator, "|"))
                    tailexpression = this.ParseSimpleExpression();

                this.ParseToken(TokenType.Separator, "]");
                expression = new ListExpression(expressions, tailexpression);
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

        private bool TryParseToken(TokenType type, string value)
        {
            Token token = this.NextToken();

            if (token == null)
                return false;

            if (token.Type == type && token.Value == value)
                return true;

            this.PushToken(token);

            return false;
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

        private bool IsBinaryOperator(int level, Token token)
        {
            return (token.Type == TokenType.Operator || token.Type == TokenType.Atom) && binaryoperators[level].Contains(token.Value);
        }
    }
}
