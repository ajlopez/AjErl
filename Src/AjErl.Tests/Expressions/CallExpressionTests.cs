namespace AjErl.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Language;
    using AjErl.Expressions;

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
    }
}
