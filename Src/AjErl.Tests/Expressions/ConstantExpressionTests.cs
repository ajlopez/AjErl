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
            Atom atom = new Atom("atom");
            ConstantExpression expr = new ConstantExpression(atom);

            Assert.AreEqual(atom, expr.Value);
        }

        [TestMethod]
        public void EvaluateSimpleConstantExpression()
        {
            Atom atom = new Atom("atom");
            ConstantExpression expr = new ConstantExpression(atom);

            Assert.AreEqual(atom, expr.Evaluate(null));
        }
    }
}
