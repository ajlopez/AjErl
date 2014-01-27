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
    public class CompositeExpressionTests
    {
        [TestMethod]
        public void CreateAndEvaluateCompositeExpressionWithTwoConstants()
        {
            IExpression expr1 = new ConstantExpression(1);
            IExpression expr2 = new ConstantExpression(2);

            CompositeExpression expr = new CompositeExpression(new IExpression[] { expr1, expr2 });

            Assert.IsFalse(expr.HasVariable());
            Assert.IsNotNull(expr.Expressions);
            Assert.AreEqual(2, expr.Expressions.Count);
            Assert.AreSame(expr1, expr.Expressions[0]);
            Assert.AreSame(expr2, expr.Expressions[1]);

            var result = expr.Evaluate(null);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void CreateAndEvaluateCompositeExpressionWithTwoVariables()
        {
            IExpression expr1 = new VariableExpression(new Variable("X"));
            IExpression expr2 = new VariableExpression(new Variable("Y"));
            Context context = new Context();
            context.SetValue("X", 1);
            context.SetValue("Y", 2);

            CompositeExpression expr = new CompositeExpression(new IExpression[] { expr1, expr2 });

            Assert.IsTrue(expr.HasVariable());
            Assert.IsNotNull(expr.Expressions);
            Assert.AreEqual(2, expr.Expressions.Count);
            Assert.AreSame(expr1, expr.Expressions[0]);
            Assert.AreSame(expr2, expr.Expressions[1]);

            var result = expr.Evaluate(context);

            Assert.AreEqual(2, result);
        }
    }
}
