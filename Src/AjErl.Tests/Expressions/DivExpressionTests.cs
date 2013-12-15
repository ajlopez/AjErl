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
        public void DivideIntegerByDouble()
        {
            DivExpression expr = new DivExpression(new ConstantExpression(2), new ConstantExpression(2.5));

            Assert.AreEqual(2 / 2.5, expr.Evaluate(null));
        }

        [TestMethod]
        public void DivideDoubleByInteger()
        {
            DivExpression expr = new DivExpression(new ConstantExpression(2.5), new ConstantExpression(3));

            Assert.AreEqual(2.5 / 3, expr.Evaluate(null));
        }

        [TestMethod]
        public void DivideTwoDoubles()
        {
            DivExpression expr = new DivExpression(new ConstantExpression(2.5), new ConstantExpression(3.7));

            Assert.AreEqual(2.5 / 3.7, expr.Evaluate(null));
        }
    }
}
