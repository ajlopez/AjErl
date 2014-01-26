namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class FuncFunction : IFunction
    {
        private Func<Context, IList<object>, object> func;

        public FuncFunction(Func<Context, IList<object>, object> func)
        {
            this.func = func;
        }

        public object Apply(Context context, IList<object> arguments)
        {
            return this.func(context, arguments);
        }
    }
}
