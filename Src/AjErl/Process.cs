namespace AjErl
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using AjErl.Language;

    public class Process
    {
        private static ThreadLocal<Process> current = new ThreadLocal<Process>();

        private Mailbox mailbox = new Mailbox();

        public Process()
        {
        }

        public static Process Current { get { return current.Value; } set { current.Value = value; } }

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

        public void RejectMessage(object message)
        {
            this.mailbox.Reject(message);
        }

        private void Run(object function)
        {
            Process.Current = this;
            ((IFunction)function).Apply(null, new object[] { });
        }
    }
}
