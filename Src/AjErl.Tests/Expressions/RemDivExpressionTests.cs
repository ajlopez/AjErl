namespace AjErl.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RemExpressionTests
    {
        [TestMethod]
        public void RemainderOfTwoIntegers()
        {
            RemExpression expr = new RemExpression(new ConstantExpression(6), new ConstantExpression(2));

            Assert.AreEqual(0, expr.Evaluate(null));
        }

        [TestMethod]
        public void NonZeroRemainderOfTwoIntegers()
        {
            RemExpression expr = new RemExpression(new ConstantExpression(5), new ConstantExpression(2));

            Assert.AreEqual(1, expr.Evaluate(null));
        }

        [TestMethod]
        public void RaiseIfRemainderIntegerByReal()
        {
            RemExpression expr = new RemExpression(new ConstantExpression(6), new ConstantExpression(2.5));

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
        public void RaiseIfRemainderRealByInteger()
        {
            RemExpression expr = new RemExpression(new ConstantExpression(6.5), new ConstantExpression(2));

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
