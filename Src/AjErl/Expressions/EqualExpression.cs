namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EqualExpression : BinaryExpression
    {
        public EqualExpression(IExpression left, IExpression right)
            : base(left, right)
        {
        }

        public override object Apply(object leftvalue, object rightvalue)
        {
            if (leftvalue == null)
                return rightvalue == null;

            if (leftvalue is int && rightvalue is double)
                return ((int)leftvalue + 0.0).Equals(rightvalue);

            if (leftvalue is double && rightvalue is int)
                return leftvalue.Equals((int)rightvalue + 0.0);

            return leftvalue.Equals(rightvalue);
        }
    }
}
