namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;

    public class Function
    {
        private IList<object> parameters;
        private IExpression body;

        public Function(IList<object> parameters, IExpression body)
        {
            this.parameters = parameters;
            this.body = body;
        }

        public Context MakeContext(IList<object> arguments)
        {
            if (this.parameters.Count != arguments.Count)
                return null;

            Context context = new Context();

            for (int k = 0; k < this.parameters.Count; k++)
                if (!Match.MatchObjects(this.parameters[k], arguments[k], context))
                    return null;

            return context;
        }

        public object Evaluate(Context context)
        {
            return this.body.Evaluate(context);
        }
    }
}
