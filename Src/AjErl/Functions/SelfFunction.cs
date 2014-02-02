namespace AjErl.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class SelfFunction : IFunction
    {
        public object Apply(Context context, IList<object> arguments)
        {
            return Process.Current;
        }
    }
}
