namespace AjErl.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DivExpressionTests
    {
        [TestMethod]
        public void DivideTwoIntegers()
        {
            DivExpression expr = new DivExpression(new ConstantExpression(6), new ConstantExpression(2));

            Assert.AreEqual(3, expr.Evaluate(null));
        }

        [TestMethod]
        public void DivideTwoIntegersWithTruncation()
        {
            DivExpression expr = new DivExpression(new ConstantExpression(5), new ConstantExpression(2));

            Assert.AreEqual(2, expr.Evaluate(null));
        }

        [TestMethod]
        public void RaiseIfDivideIntegerByReal()
        {
            DivExpression expr = new DivExpression(new ConstantExpression(6), new ConstantExpression(2.5));

            try
            {
                expr.Evaluate(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidCastException));
            }
        }

        [TestMethod]
        public void RaiseIfDivideRealByInteger()
        {
            DivExpression expr = new DivExpression(new ConstantExpression(6.5), new ConstantExpression(2));

            try
            {
                expr.Evaluate(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidCastException));
            }
        }
    }
}
