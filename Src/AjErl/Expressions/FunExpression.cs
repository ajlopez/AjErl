namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class FunExpression : IExpression
    {
        private IList<IExpression> parameterexpressions;
        private IExpression body;

        public FunExpression(IList<IExpression> parameterexpressions, IExpression body)
        {
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

        public IList<IExpression> ParameterExpressions { get { return this.parameterexpressions; } }

        public IExpression Body { get { return this.body; } }

        public object Evaluate(Context context, bool withvars = false)
        {
            Context newcontext = new Context();
            IList<object> parameters = new List<object>();

            foreach (var pexpr in this.parameterexpressions)
                parameters.Add(pexpr.Evaluate(newcontext, true));

            var func = new Function(context, parameters, this.body);

            return func;
        }

        public bool HasVariable()
        {
            return false;
        }
    }
}
