namespace AjErl.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Language;
    using AjErl.Compiler;
    using AjErl.Expressions;

    [TestClass]
    public class MatchTests
    {
        [TestMethod]
        public void MatchAtoms()
        {
            Match match = new Match(null, new Atom("a"), new ConstantExpression(1));

            var context = match.MakeContext(new Atom("a"));

            Assert.IsNotNull(context);

            var result = match.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DontMatchDifferentAtoms()
        {
            Match match = new Match(null, new Atom("a"), new ConstantExpression(1));

            var context = match.MakeContext(new Atom("b"));

            Assert.IsNull(context);
        }

        [TestMethod]
        public void MatchVariableInteger()
        {
            Match match = new Match(null, new Variable("X"), new VariableExpression(new Variable("X")));

            var context = match.MakeContext(123);

            Assert.IsNotNull(context);

            var result = match.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.AreEqual(123, result);
        }
    }
}
