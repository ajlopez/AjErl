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

        public ListExpression(IEnumerable<IExpression> expressions)
        {
            this.expressions = new System.Collections.Generic.List<IExpression>(expressions);
        }

        public IList<IExpression> Expressions { get { return this.expressions; } }

        public object Evaluate(Context context, bool withvars = false)
        {
            IList<object> elements = new List<object>();

            foreach (var expr in this.expressions)
                elements.Add(expr.Evaluate(context, withvars));

            return List.MakeList(elements);
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
