namespace AjErl.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;

    public class FunctionDefinition : IForm
    {
        private string name;
        private IList<IExpression> arguments;
        private IExpression body;

        public FunctionDefinition(string name, IList<IExpression> arguments, IExpression body)
        {
            this.name = name;
            this.arguments = arguments;
            this.body = body;
        }

        public string Name { get { return this.name; } }

        public IList<IExpression> Arguments { get { return this.arguments; } }

        public IExpression Body { get { return this.body; } }
    }
}
