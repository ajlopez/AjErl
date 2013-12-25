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
            Function function = new Function(null, new object[] { }, new ConstantExpression(1));

            Assert.AreEqual(1, function.Evaluate(null));
        }

        [TestMethod]
        public void EvaluateExpressionBody()
        {
            Function function = new Function(null, new object[] { }, new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y"))));
            Context context = new Context();
            context.SetValue("X", 1);
            context.SetValue("Y", 2);

            Assert.AreEqual(3, function.Evaluate(context));
        }

        [TestMethod]
        public void MakeContextAndEvaluateExpressionBody()
        {
            Function function = new Function(null, new object[] { new Variable("X"), new Variable("Y") }, new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y"))));

            Context context = function.MakeContext(new object[] { 1, 2 });

            Assert.IsNotNull(context);
            Assert.AreEqual(1, context.GetValue("X"));
            Assert.AreEqual(2, context.GetValue("Y"));

            Assert.AreEqual(3, function.Evaluate(context));
        }

        [TestMethod]
        public void CannotMakeContextByArity()
        {
            Function function = new Function(null, new object[] { new Variable("X"), new Variable("Y") }, new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("Y"))));

            Context context = function.MakeContext(new object[] { 1, 2, 3 });

            Assert.IsNull(context);
        }

        [TestMethod]
        public void CannotMakeContextByNoMatch()
        {
            Function function = new Function(null, new object[] { new Variable("X"), new Variable("X") }, new AddExpression(new VariableExpression(new Variable("X")), new VariableExpression(new Variable("X"))));

            Context context = function.MakeContext(new object[] { 1, 2 });

            Assert.IsNull(context);
        }
    }
}
