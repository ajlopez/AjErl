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
    public class MatchBodyExpressionTests
    {
        [TestMethod]
        public void MatchBodyMessage()
        {
            Context context = new Context();
            MatchBodyExpression expr = new MatchBodyExpression(new VariableExpression(new Variable("X")), new ConstantExpression(1));

            Assert.IsNotNull(expr.MatchExpression);
            Assert.IsInstanceOfType(expr.MatchExpression, typeof(VariableExpression));
            Assert.IsNotNull(expr.BodyExpression);
            Assert.IsInstanceOfType(expr.BodyExpression, typeof(ConstantExpression));

            Assert.IsTrue(expr.HasVariable());
            
            var result = expr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MatchBody));

            var mbody = (MatchBody)result;
            Assert.IsNotNull(mbody.Head);
            Assert.IsInstanceOfType(mbody.Head, typeof(Variable));
            Assert.IsNotNull(mbody.Body);
            Assert.IsInstanceOfType(mbody.Body, typeof(ConstantExpression));
        }

        [TestMethod]
        public void MatchBodyMessageToProcessWithVariableExpression()
        {
            Context context = new Context();

            MatchBodyExpression expr = new MatchBodyExpression(new AtomExpression(new Atom("a")), new VariableExpression(new Variable("X")));

            Assert.IsNotNull(expr.MatchExpression);
            Assert.IsInstanceOfType(expr.MatchExpression, typeof(AtomExpression));
            Assert.IsNotNull(expr.BodyExpression);
            Assert.IsInstanceOfType(expr.BodyExpression, typeof(VariableExpression));

            Assert.IsTrue(expr.HasVariable());

            var result = expr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MatchBody));

            var mbody = (MatchBody)result;
            Assert.IsNotNull(mbody.Head);
            Assert.IsInstanceOfType(mbody.Head, typeof(Atom));
            Assert.IsNotNull(mbody.Body);
            Assert.IsInstanceOfType(mbody.Body, typeof(VariableExpression));
        }
    }
}
