namespace AjErl.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using AjErl.Language;

    public class VariableExpression : IExpression
    {
        private Variable variable;

        public VariableExpression(Variable variable)
        {
            this.variable = variable;
        }

        public object Evaluate(Context context)
        {
            return context.GetValue(this.variable.Name);
        }
    }
}
