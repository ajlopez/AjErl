namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StrictEqualExpression : BinaryExpression
    {
        public StrictEqualExpression(IExpression left, IExpression right)
            : base(left, right)
        {
        }

        public override object Apply(object leftvalue, object rightvalue)
        {
            if (leftvalue == null)
                return rightvalue == null;

            return leftvalue.Equals(rightvalue);
        }
    }
}
