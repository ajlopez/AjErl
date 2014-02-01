namespace AjErl.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Compiler;
    using AjErl.Expressions;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MultiFunExpressionTests
    {
        [TestMethod]
        public void EvaluateWithOneExpression()
        {
            Context context = new Context();
            FunExpression expr = this.MakeExpression("fun(0) -> 1 end.");
            MultiFunExpression mexpr = new MultiFunExpression(new FunExpression[] { expr });

            Assert.IsFalse(mexpr.HasVariable());

            var result = mexpr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MultiFunction));

            var mfunc = (MultiFunction)result;

            Assert.IsNotNull(mfunc.Functions);
            Assert.AreEqual(1, mfunc.Functions.Count);
        }

        [TestMethod]
        public void EvaluateWithThreeExpressions()
        {
            Context context = new Context();
            FunExpression expr1 = this.MakeExpression("fun(0) -> 1 end.");
            FunExpression expr2 = this.MakeExpression("fun(1) -> 1 end.");
            FunExpression expr3 = this.MakeExpression("fun(X) -> f(X-1) + f(X-2) end.");
            MultiFunExpression mexpr = new MultiFunExpression(new FunExpression[] { expr1, expr2, expr3 });

            var result = mexpr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MultiFunction));

            var mfunc = (MultiFunction)result;

            Assert.IsNotNull(mfunc.Functions);
            Assert.AreEqual(3, mfunc.Functions.Count);
        }

        [TestMethod]
        public void RaiseIfArityIsWrong()
        {
            Context context = new Context();
            FunExpression expr1 = this.MakeExpression("fun(0) -> 1 end.");
            FunExpression expr2 = this.MakeExpression("fun(1) -> 1 end.");
            FunExpression expr3 = this.MakeExpression("fun(X, Y) -> f(X-1) + f(X-2) end.");
            MultiFunExpression mexpr = new MultiFunExpression(new FunExpression[] { expr1, expr2, expr3 });

            try
            {
                mexpr.Evaluate(context);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("head mismatch", ex.Message);
            }
        }

        private FunExpression MakeExpression(string text)
        {
            Context context = new Context();
            Parser parser = new Parser(text);
            return (FunExpression)parser.ParseExpression();
        }
    }
}
