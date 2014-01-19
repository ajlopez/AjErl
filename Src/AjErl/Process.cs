namespace AjErl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using AjErl.Language;

    public class Process
    {
        public Process()
        {
        }

        public void Start(IFunction function)
        {
            ParameterizedThreadStart pts = new ParameterizedThreadStart(this.Run);
            Thread thread = new Thread(pts);
            thread.Start(function);
        }

        private void Run(object function)
        {
            ((IFunction)function).Apply(null, null);
        }
    }
}
