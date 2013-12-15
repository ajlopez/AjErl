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

        public IList<IExpression> Expressions { get { return this.expressions; } }

        public object Evaluate(Context context)
        {
            IList<object> elements = new List<object>();

            foreach (var expr in this.expressions)
            {
                var value = expr.Evaluate(context);

                if (value is Variable)
                    throw new Exception(string.Format("variable '{0}' is unbound", ((Variable)value).Name));

                elements.Add(value);
            }

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
