namespace AjErl.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MultiplyExpressionTests
    {
        [TestMethod]
        public void MultiplyTwoIntegers()
        {
            MultiplyExpression expr = new MultiplyExpression(new ConstantExpression(3), new ConstantExpression(2));

            Assert.AreEqual(6, expr.Evaluate(null));
        }

        [TestMethod]
        public void MultiplyIntegerByDouble()
        {
            MultiplyExpression expr = new MultiplyExpression(new ConstantExpression(2), new ConstantExpression(2.5));

            Assert.AreEqual(2 * 2.5, expr.Evaluate(null));
        }

        [TestMethod]
        public void MultiplyDoubleByInteger()
        {
            MultiplyExpression expr = new MultiplyExpression(new ConstantExpression(2.5), new ConstantExpression(3));

            Assert.AreEqual(2.5 * 3, expr.Evaluate(null));
        }

        [TestMethod]
        public void MultiplyTwoDoubles()
        {
            MultiplyExpression expr = new MultiplyExpression(new ConstantExpression(2.5), new ConstantExpression(3.7));

            Assert.AreEqual(2.5 * 3.7, expr.Evaluate(null));
        }
    }
}
