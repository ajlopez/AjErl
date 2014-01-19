namespace AjErl.Tests.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using AjErl.Functions;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SpawnFunctionTests
    {
        [TestMethod]
        public void SpawnSimpleProcess()
        {
            AutoResetEvent handle = new AutoResetEvent(false);
            int count = 0;
            IFunction func = new SpawnFunction();
            IFunction lambda = new LambdaFunction(() => { count = 1; return handle.Set(); });

            var result = func.Apply(null, new object[] { lambda });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Process));

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
