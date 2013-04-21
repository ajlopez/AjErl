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
    public class VariableExpressionTests
    {
        [TestMethod]
        public void CreateSimpleVariableExpression()
        {
            Variable variable = new Variable("X");
            VariableExpression expression = new VariableExpression(variable);

            Assert.AreEqual(variable, expression.Variable);
        }

        [TestMethod]
        public void EvaluateVariableExpression()
        {
            Variable variable = new Variable("X");
            Context context = new Context();
            context.SetValue("X", 1);
            VariableExpression expression = new VariableExpression(variable);

            Assert.AreEqual(1, expression.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateUndefinedVariableExpression()
        {
            Variable variable = new Variable("X");
            Context context = new Context();
            VariableExpression expression = new VariableExpression(variable);

            Assert.AreEqual(variable, expression.Evaluate(context));
        }
    }
}

