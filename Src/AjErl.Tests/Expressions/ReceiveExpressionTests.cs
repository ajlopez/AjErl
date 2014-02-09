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
    public class ReceiveExpressionTests
    {
        [TestMethod]
        public void ReceiveMessage()
        {
            Process process = new Process();
            process.Tell(new Atom("ping"));
            Process.Current = process;

            MatchBody match = new MatchBody(new Atom("ping"), new ConstantExpression("pong"));
            ReceiveExpression expr = new ReceiveExpression(new MatchBody[] { match });

            Assert.IsFalse(expr.HasVariable());

            var result = expr.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.AreEqual("pong", result);
        }

        [TestMethod]
        public void ReceiveMessageWithNoMatch()
        {
            Process process = new Process();
            process.Tell(new Atom("foo"));
            Process.Current = process;

            MatchBody match = new MatchBody(new Atom("ping"), new ConstantExpression("pong"));
            ReceiveExpression expr = new ReceiveExpression(new MatchBody[] { match });

            Assert.IsFalse(expr.HasVariable());

            Assert.IsNull(expr.Evaluate(null));
        }
    }
}
