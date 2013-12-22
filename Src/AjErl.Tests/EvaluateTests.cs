namespace AjErl.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Compiler;
    using AjErl.Expressions;

    [TestClass]
    public class EvaluateTests
    {
        private Context context;

        [TestInitialize]
        public void Setup()
        {
            this.context = new Context();
        }

        [TestMethod]
        public void EvaluateInteger()
        {
            Assert.AreEqual(1, this.Evaluate("1."));
        }

        [TestMethod]
        public void EvaluateSum()
        {
            Assert.AreEqual(3, this.Evaluate("1+2."));
        }

        [TestMethod]
        public void EvaluateVariableMatch()
        {
            Assert.AreEqual(3, this.Evaluate("X=1+2."));
            Assert.AreEqual(3, this.context.GetValue("X"));
        }

        private object Evaluate(string text)
        {
            Parser parser = new Parser(text);
            IExpression expression = parser.ParseExpression();
            return expression.Evaluate(this.context);
        }
    }
}
