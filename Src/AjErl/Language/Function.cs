namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;

    public class Function
    {
        private IExpression body;

        public Function(IExpression body)
        {
            this.body = body;
        }

        public object Evaluate(Context context)
        {
            return this.body.Evaluate(context);
        }
    }
}
