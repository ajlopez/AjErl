namespace AjErl.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class SpawnFunction : IFunction
    {
        public object Apply(Context context, IList<object> arguments)
        {
            IFunction func = (IFunction)arguments[0];
            Process process = new Process();
            process.Start(func);
            return process;
        }
    }
}
