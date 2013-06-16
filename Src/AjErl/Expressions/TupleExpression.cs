namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class TupleExpression : IExpression
    {
        private IList<IExpression> expressions;

        public TupleExpression(IEnumerable<IExpression> expressions)
        {
            this.expressions = new System.Collections.Generic.List<IExpression>(expressions);
        }

        public object Evaluate(Context context)
        {
            IList<object> elements = new List<object>();

            foreach (var expr in this.expressions)
                elements.Add(expr.Evaluate(context));

            return new Tuple(elements);
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
