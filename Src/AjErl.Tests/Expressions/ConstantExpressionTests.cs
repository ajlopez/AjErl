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
    public class ConstantExpressionTests
    {
        [TestMethod]
        public void CreateSimpleConstantExpression()
        {
            ConstantExpression expr = new ConstantExpression(10);

            Assert.AreEqual(10, expr.Value);
        }

        [TestMethod]
        public void EvaluateSimpleConstantExpression()
        {
            ConstantExpression expr = new ConstantExpression(10);

            Assert.AreEqual(10, expr.Evaluate(null));
        }
    }
}
