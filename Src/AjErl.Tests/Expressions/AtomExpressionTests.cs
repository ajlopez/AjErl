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

