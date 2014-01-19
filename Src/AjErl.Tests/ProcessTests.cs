namespace AjErl.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Language;
    using System.Threading;

    [TestClass]
    public class ProcessTests
    {
        [TestMethod]
        public void ProcessStartAndRun()
        {
            AutoResetEvent handle = new AutoResetEvent(false);
            int count = 0;
            IFunction function = new LambdaFunction(() => { count = 1; return handle.Set(); });
            Process process = new Process();

            process.Start(function);

            handle.WaitOne();

            Assert.AreEqual(1, count);
        }

        private class LambdaFunction : IFunction
        {
            private Func<object> function;

            public LambdaFunction(Func<object> function)
            {
                this.function = function;
            }

            public object Apply(Context context, IList<object> arguments)
            {
                return this.function();
            }
        }
    }
}
