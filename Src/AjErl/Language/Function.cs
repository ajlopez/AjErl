namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;

    public class Function : IFunction
    {
        private Context context;
        private IList<object> parameters;
        private IExpression body;

        public Function(Context context, IList<object> parameters, IExpression body)
        {
            this.context = context;
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

            context.SetParent(this.context);

            return context;
        }

        public object Evaluate(Context context)
        {
            return this.body.Evaluate(context);
        }

        public object Apply(Context context, IList<object> arguments)
        {
            return this.body.Evaluate(this.MakeContext(arguments));
        }
    }
}
