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
        private Mailbox mailbox = new Mailbox();

        public Process()
        {
        }

        public void Start(IFunction function)
        {
            ParameterizedThreadStart pts = new ParameterizedThreadStart(this.Run);
            Thread thread = new Thread(pts);
            thread.Start(function);
        }

        public void Tell(object message)
        {
            this.mailbox.Add(message);
        }

        public object GetMessage()
        {
            return this.mailbox.Take();
        }

        private void Run(object function)
        {
            ((IFunction)function).Apply(null, null);
        }
    }
}
