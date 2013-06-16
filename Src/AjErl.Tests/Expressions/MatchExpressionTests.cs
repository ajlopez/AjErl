namespace AjErl.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void MatchTwoVariablesWithList()
        {
            Context context = new Context();
            Variable headvar = new Variable("H");
            Variable tailvar = new Variable("T");
            var list = new List(headvar, tailvar);
            var list2 = new List(1, new List(2, new List(3, null)));

            MatchExpression expr = new MatchExpression(new ConstantExpression(list), new ConstantExpression(list2));

            expr.Evaluate(context);

            var result1 = context.GetValue("H");
            var result2 = context.GetValue("T");

            Assert.IsNotNull(result1);
            Assert.AreEqual(1, result1);
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(List));
            Assert.AreEqual("[2, 3]", result2.ToString());
        }
    }
}
