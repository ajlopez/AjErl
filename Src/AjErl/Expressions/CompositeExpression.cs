namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CompositeExpression : IExpression
    {
        private IList<IExpression> expressions;

        public CompositeExpression(IList<IExpression> expressions)
        {
            this.expressions = expressions;
        }

        public IList<IExpression> Expressions { get { return this.expressions; } }

        public object Evaluate(Context context, bool withvars = false)
        {
            object result = null;

            foreach (var expr in this.expressions)
                result = expr.Evaluate(context, withvars);

            return result;
        }

        public bool HasVariable()
        {
            foreach (var expr in this.expressions)
                if (expr.HasVariable())
                    return true;

            return false;
        }
    }
}
