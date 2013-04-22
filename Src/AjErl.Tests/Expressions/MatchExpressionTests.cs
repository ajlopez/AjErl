namespace AjErl.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Expressions;
    using AjErl.Language;

    [TestClass]
    public class MatchExpressionTests
    {
        [TestMethod]
        public void MatchIntegers()
        {
            MatchExpression expr = new MatchExpression(new ConstantExpression(123), new ConstantExpression(123));

            Assert.IsNull(expr.Evaluate(null));
        }

        [TestMethod]
        public void HasVariablesWithIntegers()
        {
            MatchExpression expr = new MatchExpression(new ConstantExpression(123), new ConstantExpression(123));

            Assert.IsFalse(expr.HasVariable());
        }

        [TestMethod]
        public void NoMatchWithException()
        {
            MatchExpression expr = new MatchExpression(new ConstantExpression(123), new ConstantExpression("foo"));

            try
            {
                expr.Evaluate(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
                Assert.AreEqual("invalid match", ex.Message);
            }
        }

        [TestMethod]
        public void MatchVariableWithInteger()
        {
            Context context = new Context();
            Variable variable = new Variable("X");
            MatchExpression expr = new MatchExpression(new VariableExpression(variable), new ConstantExpression(123));

            Assert.IsNull(expr.Evaluate(context));

            Assert.AreEqual(123, context.GetValue("X"));
        }

        [TestMethod]
        public void HasVariablesWithVariable()
        {
            Variable variable = new Variable("X");
            MatchExpression expr = new MatchExpression(new VariableExpression(variable), new ConstantExpression(123));

            Assert.IsTrue(expr.HasVariable());
        }
    }
}
