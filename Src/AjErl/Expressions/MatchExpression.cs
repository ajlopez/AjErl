namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class MatchExpression : IExpression
    {
        private IExpression leftexpr;
        private IExpression rightexpr;

        public MatchExpression(IExpression leftexpr, IExpression rightexpr)
        {
            this.leftexpr = leftexpr;
            this.rightexpr = rightexpr;
        }

        public IExpression LeftExpression { get { return this.leftexpr; } }

        public IExpression RightExpression { get { return this.rightexpr; } }

        public object Evaluate(Context context)
        {
            object left = this.leftexpr.Evaluate(context);
            object right = this.rightexpr.Evaluate(context);

            if (!Match.MatchObjects(left, right, context))
                throw new InvalidOperationException("invalid match");

            return right;
        }

        public bool HasVariable()
        {
            return this.leftexpr.HasVariable() || this.rightexpr.HasVariable();
        }
    }
}
