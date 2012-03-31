using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjErl.Expressions;
using AjErl.Language;

namespace AjErl.Tests.Expressions
{
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
