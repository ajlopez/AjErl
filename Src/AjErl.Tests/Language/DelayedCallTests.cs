namespace AjErl.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Language;
    using AjErl.Compiler;

    [TestClass]
    public class DelayedCallTests
    {
        [TestMethod]
        public void CreateAndEvaluateDelayedCall()
        {
            IFunction function = this.MakeFunction("f(X) -> X+1.");
            Context context = new Context();
            var arguments = new object[] { 1 };

            DelayedCall dcall = new DelayedCall(function, context, arguments);

            Assert.IsNotNull(dcall.Function);
            Assert.AreSame(function, dcall.Function);
            Assert.AreSame(context, dcall.Context);
            Assert.AreSame(arguments, dcall.Arguments);

            Assert.AreEqual(2, dcall.Evaluate());
        }

        private IFunction MakeFunction(string text)
        {
            Context context = new Context();
            Parser parser = new Parser(text);
            var form = parser.ParseForm();
            return (IFunction)form.Evaluate(context);
        }
    }
}
