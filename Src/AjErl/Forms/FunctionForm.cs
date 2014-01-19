namespace AjErl.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using AjErl.Language;

    public class FunctionForm : IForm
    {
        private string name;
        private IList<IExpression> parameterexpressions;
        private IExpression body;

        public FunctionForm(string name, IList<IExpression> parameterexpressions, IExpression body)
        {
            this.name = name;
            this.parameterexpressions = parameterexpressions;
            this.body = body;

            if (body is CallExpression)
                this.body = ((CallExpression)body).ToDelayedCallExpression();
            else if (body is CompositeExpression)
            {
                var cexpr = (CompositeExpression)body;
                var last = cexpr.Expressions.Count - 1;

                if (cexpr.Expressions[last] is CallExpression)
                    cexpr.Expressions[last] = ((CallExpression)cexpr.Expressions[last]).ToDelayedCallExpression();
            }
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

            var func = new Function(context, parameters, this.body);

            context.SetValue(string.Format("{0}/{1}", this.name, parameters.Count), func);

            return func;
        }
    }
}
