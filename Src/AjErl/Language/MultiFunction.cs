namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;

    public class MultiFunction : IFunction
    {
        private IList<Function> functions;

        public MultiFunction(IList<Function> functions)
        {
            this.functions = functions;
        }

        public object Apply(Context context, IList<object> arguments)
        {
            foreach (var function in this.functions)
            {
                var newcontext = function.MakeContext(arguments);

                if (newcontext != null)
                    return function.Evaluate(newcontext);
            }

            throw new Exception("no function clause to match");
        }
    }
}
