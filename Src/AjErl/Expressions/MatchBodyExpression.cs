namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class MatchBodyExpression : IExpression
    {
        private IExpression matchexpr;
        private IExpression bodyexpr;

        public MatchBodyExpression(IExpression matchexpr, IExpression bodyexpr)
        {
            this.matchexpr = matchexpr;
            this.bodyexpr = bodyexpr;
        }

        public IExpression MatchExpression { get { return this.matchexpr; } }

        public IExpression BodyExpression { get { return this.bodyexpr; } }

        public object Evaluate(Context context, bool withvars = false)
        {
            object match = this.matchexpr.Evaluate(context, true);

            return new MatchBody(context, match, this.bodyexpr);
        }

        public bool HasVariable()
        {
            return this.matchexpr.HasVariable() || this.bodyexpr.HasVariable();
        }
    }
}
