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
    public class FunExpressionTests
    {
        [TestMethod]
        public void DefineAndEvaluateFunction()
        {
            FunExpression expr = new FunExpression(new IExpression[] { new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y")) }, new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y"))));
            Context context = new Context();

            Assert.IsFalse(expr.HasVariable());
            var result = expr.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Function));

            var func = (Function)result;

            var newcontext = func.MakeContext(new object[] { 1, 2 });

            Assert.IsNotNull(newcontext);
            Assert.AreEqual(1, newcontext.GetValue("X"));
            Assert.AreEqual(2, newcontext.GetValue("Y"));

            Assert.AreEqual(3, func.Evaluate(newcontext));
        }
    }
}
