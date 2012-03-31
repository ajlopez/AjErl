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

        public Variable Variable { get { return this.variable; } }

        public object Evaluate(Context context)
        {
            if (!context.HasValue(this.variable.Name))
                return this.variable;

            return context.GetValue(this.variable.Name);
        }

        public bool HasVariable()
        {
            return true;
        }
    }
}
