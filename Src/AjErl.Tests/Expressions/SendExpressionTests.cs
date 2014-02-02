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
    public class SendExpressionTests
    {
        [TestMethod]
        public void SendMessage()
        {
            Context context = new Context();
            Process process = new Process();
            context.SetValue("X", process);

            SendExpression expr = new SendExpression(new VariableExpression(new Variable("X")), new ConstantExpression(1));

            Assert.IsNotNull(expr.ProcessExpression);
            Assert.IsInstanceOfType(expr.ProcessExpression, typeof(VariableExpression));
            Assert.IsNotNull(expr.MessageExpression);
            Assert.IsInstanceOfType(expr.MessageExpression, typeof(ConstantExpression));

            Assert.IsTrue(expr.HasVariable());
            Assert.AreEqual(1, expr.Evaluate(context));
            Assert.AreEqual(1, process.GetMessage());
        }

        [TestMethod]
        public void SendMessageToProcessWithVariableExpression()
        {
            Context context = new Context();
            Process process = new Process();
            context.SetValue("X", 1);

            SendExpression expr = new SendExpression(new ConstantExpression(process), new VariableExpression(new Variable("X")));

            Assert.IsNotNull(expr.ProcessExpression);
            Assert.IsInstanceOfType(expr.ProcessExpression, typeof(ConstantExpression));
            Assert.IsNotNull(expr.MessageExpression);
            Assert.IsInstanceOfType(expr.MessageExpression, typeof(VariableExpression));

            Assert.IsTrue(expr.HasVariable());
            Assert.AreEqual(1, expr.Evaluate(context));
            Assert.AreEqual(1, process.GetMessage());
        }
    }
}
