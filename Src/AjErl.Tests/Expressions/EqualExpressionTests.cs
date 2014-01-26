namespace AjErl.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Expressions;

    [TestClass]
    public class EqualExpressionTests
    {
        [TestMethod]
        public void CompareOneWithOne()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression(1), new ConstantExpression(1));

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareOneWithTwo()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression(1), new ConstantExpression(2));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareFooWithFoo()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression("foo"), new ConstantExpression("foo"));

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareFooWithBar()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression("foo"), new ConstantExpression("bar"));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareNullWithBar()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression(null), new ConstantExpression("bar"));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareNullWithNull()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression(null), new ConstantExpression(null));

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareIntegerOneWithRealOne()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression(1), new ConstantExpression(1.0));

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareRealOneWithIntegerOne()
        {
            EqualExpression expr = new EqualExpression(new ConstantExpression(1.0), new ConstantExpression(1));

            Assert.AreEqual(true, expr.Evaluate(null));
        }
    }
}
