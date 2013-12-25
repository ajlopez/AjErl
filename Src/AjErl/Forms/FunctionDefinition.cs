namespace AjErl.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using AjErl.Language;

    public class FunctionDefinition : IForm
    {
        private string name;
        private IList<IExpression> parameterexpressions;
        private IExpression body;

        public FunctionDefinition(string name, IList<IExpression> parameterexpressions, IExpression body)
        {
            this.name = name;
            this.parameterexpressions = parameterexpressions;
            this.body = body;
        }

        public string Name { get { return this.name; } }

        public IList<IExpression> ParameterExpressions { get { return this.parameterexpressions; } }

        public IExpression Body { get { return this.body; } }

        public object Evaluate(Context context)
        {
            Context newcontext = new Context();
            IList<object> parameters = new List<object>();

            foreach (var pexpr in this.parameterexpressions)
                parameters.Add(pexpr.Evaluate(newcontext, true));

            var func = new Function(context, parameters, body);

            context.SetValue(this.name, func);

            return func;
        }
    }
}
