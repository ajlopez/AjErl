namespace AjErl.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Expressions;

    [TestClass]
    public class StrictEqualExpressionTests
    {
        [TestMethod]
        public void CompareOneWithOne()
        {
            StrictEqualExpression expr = new StrictEqualExpression(new ConstantExpression(1), new ConstantExpression(1));

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareOneWithTwo()
        {
            StrictEqualExpression expr = new StrictEqualExpression(new ConstantExpression(1), new ConstantExpression(2));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareFooWithFoo()
        {
            StrictEqualExpression expr = new StrictEqualExpression(new ConstantExpression("foo"), new ConstantExpression("foo"));

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareFooWithBar()
        {
            StrictEqualExpression expr = new StrictEqualExpression(new ConstantExpression("foo"), new ConstantExpression("bar"));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareNullWithBar()
        {
            StrictEqualExpression expr = new StrictEqualExpression(new ConstantExpression(null), new ConstantExpression("bar"));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareNullWithNull()
        {
            StrictEqualExpression expr = new StrictEqualExpression(new ConstantExpression(null), new ConstantExpression(null));

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareIntegerOneWithRealOne()
        {
            StrictEqualExpression expr = new StrictEqualExpression(new ConstantExpression(1), new ConstantExpression(1.0));

            Assert.AreEqual(false, expr.Evaluate(null));
        }
    }
}
