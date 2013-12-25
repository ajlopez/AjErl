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
                elements.Add(expr.Evaluate(context, withvars));

            List tail = null;

            if (this.tailexpression != null)
            {
                object tailvalue = this.tailexpression.Evaluate(context, withvars);

                if (tailvalue is Variable)
                    return List.MakeList(elements, (Variable)tailvalue);

                tail = (List)tailvalue;
            }

            if (elements.Count == 0 && tail == null)
                return EmptyList.Instance;

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
