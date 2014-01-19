namespace AjErl.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Compiler;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MultiFunctionTests
    {
        [TestMethod]
        public void EvaluateMultiFunctionWithOneFunction()
        {
            Function func = this.MakeFunction("f(0) -> 1.");
            MultiFunction mfunc = new MultiFunction(new Function[] { func });

            Assert.IsNotNull(mfunc.Functions);
            Assert.AreEqual(1, mfunc.Functions.Count);

            Assert.AreEqual(1, mfunc.Apply(null, new object[] { 0 }));
        }

        [TestMethod]
        public void EvaluateMultiFunctionWithTwoFunctions()
        {
            Function func1 = this.MakeFunction("f(0) -> 1.");
            Function func2 = this.MakeFunction("f(1) -> 2.");
            MultiFunction mfunc = new MultiFunction(new Function[] { func1, func2 });

            Assert.AreEqual(2, mfunc.Apply(null, new object[] { 1 }));
        }

        [TestMethod]
        public void RaiseIfNoClauseToMatch()
        {
            Function func1 = this.MakeFunction("f(0) -> 1.");
            Function func2 = this.MakeFunction("f(1) -> 2.");
            MultiFunction mfunc = new MultiFunction(new Function[] { func1, func2 });

            try
            {
                mfunc.Apply(null, new object[] { 2 });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("no function clause to match", ex.Message);
            }
        }

        private Function MakeFunction(string text)
        {
            Context context = new Context();
            Parser parser = new Parser(text);
            var form = parser.ParseForm();
            return (Function)form.Evaluate(context);
        }
    }
}
