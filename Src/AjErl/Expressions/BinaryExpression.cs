﻿namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BinaryExpression : IExpression
    {
        private IExpression left;
        private IExpression right;

        public BinaryExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public IExpression LeftExpression { get { return this.left; } }

        public IExpression RightExpression { get { return this.right; } }

        public object Evaluate(Context context, bool withvars = false)
        {
            var lvalue = Machine.ExpandDelayedCall(this.left.Evaluate(context, withvars));
            var rvalue = Machine.ExpandDelayedCall(this.right.Evaluate(context, withvars));

            return this.Apply(lvalue, rvalue);
        }

        public bool HasVariable()
        {
            return this.left.HasVariable() || this.right.HasVariable();
        }

        public abstract object Apply(object leftvalue, object rightvalue);
    }
}
