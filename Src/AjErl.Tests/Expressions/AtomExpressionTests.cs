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
    public class AtomExpressionTests
    {
        [TestMethod]
        public void CreateSimpleAtomExpression()
        {
            Atom atom = new Atom("ok");
            AtomExpression expression = new AtomExpression(atom);

            Assert.AreEqual(atom, expression.Atom);
            Assert.IsFalse(expression.HasVariable());
        }

        [TestMethod]
        public void EvaluateAtomExpression()
        {
            Atom atom = new Atom("ok");
            Context context = new Context();
            AtomExpression expression = new AtomExpression(atom);

            Assert.AreEqual(atom, expression.Evaluate(context));
        }
    }
}

