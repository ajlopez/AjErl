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
    public class AddExpressionTests
    {
        [TestMethod]
        public void AddTwoIntegers()
        {
            AddExpression expr = new AddExpression(new ConstantExpression(1), new ConstantExpression(2));

            Assert.AreEqual(3, expr.Evaluate(null));
            Assert.IsFalse(expr.HasVariable());
        }

        [TestMethod]
        public void AddVariableAndInteger()
        {
            Context context = new Context();
            context.SetValue("One", 1);
            AddExpression expr = new AddExpression(new VariableExpression(new Variable("One")), new ConstantExpression(2));

            Assert.AreEqual(3, expr.Evaluate(context));
            Assert.IsTrue(expr.HasVariable());
        }

        [TestMethod]
        public void AddIntegerAndVariable()
        {
            Context context = new Context();
            context.SetValue("Two", 2);
            AddExpression expr = new AddExpression(new ConstantExpression(1), new VariableExpression(new Variable("Two")));

            Assert.AreEqual(3, expr.Evaluate(context));
            Assert.IsTrue(expr.HasVariable());
        }

        [TestMethod]
        public void AddIntegerToDouble()
        {
            AddExpression expr = new AddExpression(new ConstantExpression(1), new ConstantExpression(2.5));

            Assert.AreEqual(1 + 2.5, expr.Evaluate(null));
        }

        [TestMethod]
        public void AddDoubleToInteger()
        {
            AddExpression expr = new AddExpression(new ConstantExpression(2.5), new ConstantExpression(1));

            Assert.AreEqual(2.5 + 1, expr.Evaluate(null));
        }

        [TestMethod]
        public void AddTwoDoubles()
        {
            AddExpression expr = new AddExpression(new ConstantExpression(2.5), new ConstantExpression(3.7));

            Assert.AreEqual(2.5 + 3.7, expr.Evaluate(null));
        }
    }
}
