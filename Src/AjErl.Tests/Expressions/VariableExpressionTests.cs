using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjErl.Language;
using AjErl.Expressions;

namespace AjErl.Tests.Expressions
{
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

