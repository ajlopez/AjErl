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
    public class CallExpressionTests
    {
        [TestMethod]
        public void EvaluateCallExpression()
        {
            Context context = new Context();
            Function function = new Function(context, new object[] { new Variable("X"), new Variable("Y") }, new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y"))));
            context.SetValue("add/2", function);
            CallExpression expr = new CallExpression(new AtomExpression(new Atom("add")), new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });

            Assert.IsFalse(expr.HasVariable());
            Assert.IsNotNull(expr.NameExpression);
            Assert.IsInstanceOfType(expr.NameExpression, typeof(AtomExpression));
            Assert.IsNotNull(expr.ArgumentExpressions);
            Assert.AreEqual(2, expr.ArgumentExpressions.Count);

            Assert.AreEqual(3, expr.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateCallExpressionWithVariableInName()
        {
            Context context = new Context();
            Function function = new Function(context, new object[] { new Variable("X"), new Variable("Y") }, new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y"))));
            context.SetValue("add/2", function);
            context.SetValue("A", new Atom("add"));
            CallExpression expr = new CallExpression(new VariableExpression(new Variable("A")), new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });

            Assert.IsTrue(expr.HasVariable());
            Assert.IsNotNull(expr.NameExpression);
            Assert.IsInstanceOfType(expr.NameExpression, typeof(VariableExpression));
            Assert.IsNotNull(expr.ArgumentExpressions);
            Assert.AreEqual(2, expr.ArgumentExpressions.Count);

            Assert.AreEqual(3, expr.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateCallExpressionWithVariableInArguments()
        {
            Context context = new Context();
            Function function = new Function(context, new object[] { new Variable("X"), new Variable("Y") }, new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y"))));
            context.SetValue("add/2", function);
            context.SetValue("One", 1);
            CallExpression expr = new CallExpression(new AtomExpression(new Atom("add")), new IExpression[] { new VariableExpression(new Variable("One")), new ConstantExpression(2) });

            Assert.IsTrue(expr.HasVariable());
            Assert.IsNotNull(expr.NameExpression);
            Assert.IsInstanceOfType(expr.NameExpression, typeof(AtomExpression));
            Assert.IsNotNull(expr.ArgumentExpressions);
            Assert.AreEqual(2, expr.ArgumentExpressions.Count);

            Assert.AreEqual(3, expr.Evaluate(context));
        }
    }
}
