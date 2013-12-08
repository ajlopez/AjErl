namespace AjErl.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SubtractExpressionTests
    {
        [TestMethod]
        public void SubtractTwoIntegers()
        {
            SubtractExpression expr = new SubtractExpression(new ConstantExpression(2), new ConstantExpression(1));
            Assert.IsNotNull(expr.LeftExpression);
            Assert.IsNotNull(expr.RightExpression);
            Assert.AreEqual(1, expr.Evaluate(null));
        }

        [TestMethod]
        public void SubtractDoubleFromInteger()
        {
            SubtractExpression expr = new SubtractExpression(new ConstantExpression(1), new ConstantExpression(2.5));

            Assert.AreEqual(1 - 2.5, expr.Evaluate(null));
        }

        [TestMethod]
        public void SubtractIntegerFromDouble()
        {
            SubtractExpression expr = new SubtractExpression(new ConstantExpression(2.5), new ConstantExpression(1));

            Assert.AreEqual(2.5 - 1, expr.Evaluate(null));
        }

        [TestMethod]
        public void SubtractTwoDoubles()
        {
            SubtractExpression expr = new SubtractExpression(new ConstantExpression(2.5), new ConstantExpression(3.7));

            Assert.AreEqual(2.5 - 3.7, expr.Evaluate(null));
        }
    }
}
