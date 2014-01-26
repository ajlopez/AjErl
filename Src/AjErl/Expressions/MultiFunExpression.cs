namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class MultiFunExpression : IExpression
    {
        private IList<FunExpression> expressions;

        public MultiFunExpression(IList<FunExpression> expressions)
        {
            this.expressions = expressions;
        }

        public IList<FunExpression> Expressions { get { return this.expressions; } }

        public object Evaluate(Context context, bool withvars = false)
        {
            int arity = this.expressions[0].ParameterExpressions.Count;

            if (!this.expressions.Skip(1).All(f => f.ParameterExpressions.Count == arity))
                throw new Exception("head mismatch");

            IList<Function> functions = new List<Function>();

            foreach (var form in this.Expressions)
                functions.Add((Function)form.Evaluate(context, true));

            var func = new MultiFunction(functions);

            return func;
        }

        public bool HasVariable()
        {
            return false;
        }
    }
}
