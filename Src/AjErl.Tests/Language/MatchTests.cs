namespace AjErl.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Compiler;
    using AjErl.Expressions;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MatchTests
    {
        [TestMethod]
        public void MatchAtoms()
        {
            MatchBody match = new MatchBody(null, new Atom("a"), new ConstantExpression(1));

            var context = match.MakeContext(new Atom("a"));

            Assert.IsNotNull(context);

            var result = match.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DontMatchDifferentAtoms()
        {
            MatchBody match = new MatchBody(null, new Atom("a"), new ConstantExpression(1));

            var context = match.MakeContext(new Atom("b"));

            Assert.IsNull(context);
        }

        [TestMethod]
        public void MatchVariableInteger()
        {
            MatchBody match = new MatchBody(null, new Variable("X"), new VariableExpression(new Variable("X")));

            var context = match.MakeContext(123);

            Assert.IsNotNull(context);

            var result = match.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.AreEqual(123, result);
        }
    }
}
