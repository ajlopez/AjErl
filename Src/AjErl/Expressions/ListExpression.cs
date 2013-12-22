namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class ListExpression : IExpression
    {
        private IList<IExpression> expressions;
        private IExpression tailexpression;

        public ListExpression(IEnumerable<IExpression> expressions, IExpression tailexpression = null)
        {
            this.expressions = new System.Collections.Generic.List<IExpression>(expressions);
            this.tailexpression = tailexpression;
        }

        public IList<IExpression> Expressions { get { return this.expressions; } }

        public object Evaluate(Context context, bool withvars = false)
        {
            IList<object> elements = new List<object>();

            foreach (var expr in this.expressions)
            {
                var value = expr.Evaluate(context, withvars);

                if (!withvars && value is Variable)
                    throw new Exception(string.Format("variable '{0}' is unbound", ((Variable)value).Name));

                elements.Add(value);
            }

            List tail = null;

            if (this.tailexpression != null)
                tail = (List)this.tailexpression.Evaluate(context, withvars);

            return List.MakeList(elements, tail);
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
