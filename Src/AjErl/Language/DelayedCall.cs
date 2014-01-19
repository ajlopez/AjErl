namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DelayedCall
    {
        private IFunction function;
        private Context context;
        private IList<object> arguments;

        public DelayedCall(IFunction function, Context context, IList<object> arguments)
        {
            this.function = function;
            this.context = context;
            this.arguments = arguments;
        }

        public IFunction Function { get { return this.function; } }

        public Context Context { get { return this.context; } }

        public IList<object> Arguments { get { return this.arguments; } }

        public object Evaluate()
        {
            return this.function.Apply(context, arguments);
        }
    }
}
