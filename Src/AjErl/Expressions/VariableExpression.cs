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

        public object Evaluate(Context context, bool withvars = false)
        {
            if (!context.HasValue(this.variable.Name))
                if (!withvars)
                    throw new Exception(string.Format("variable '{0}' is unbound", this.variable.Name));
                else
                    return this.variable;

            return context.GetValue(this.variable.Name);
        }

        public bool HasVariable()
        {
            return true;
        }
    }
}
