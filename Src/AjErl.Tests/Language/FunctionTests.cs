namespace AjErl.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void EvaluateConstantBody()
        {
            Function function = new Function(new ConstantExpression(1));

            Assert.AreEqual(1, function.Evaluate(null));
        }

        [TestMethod]
        public void EvaluateExpressionBody()
        {
            Function function = new Function(new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y"))));
            Context context = new Context();
            context.SetValue("X", 1);
            context.SetValue("Y", 2);

            Assert.AreEqual(3, function.Evaluate(context));
        }
    }
}
