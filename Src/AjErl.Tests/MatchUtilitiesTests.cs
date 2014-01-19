namespace AjErl.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MatchUtilitiesTests
    {
        [TestMethod]
        public void MatchAnonymousVariable()
        {
            Context context = new Context();

            Assert.IsTrue(MatchUtilities.MatchObjects(new Variable("_"), 1, context));

            Assert.IsNull(context.GetValue("_"));
        }

        [TestMethod]
        public void MatchVariable()
        {
            Context context = new Context();

            Assert.IsTrue(MatchUtilities.MatchObjects(new Variable("X"), 1, context));

            Assert.AreEqual(1, context.GetValue("X"));
        }

        [TestMethod]
        public void MatchVariableInverse()
        {
            Context context = new Context();

            Assert.IsTrue(MatchUtilities.MatchObjects(1, new Variable("X"), context));

            Assert.AreEqual(1, context.GetValue("X"));
        }

        [TestMethod]
        public void MatchAtoms()
        {
            Context context = new Context();

            Assert.IsTrue(MatchUtilities.MatchObjects(new Atom("a"), new Atom("a"), context));
        }

        [TestMethod]
        public void NoMatchAtoms()
        {
            Context context = new Context();

            Assert.IsFalse(MatchUtilities.MatchObjects(new Atom("a"), new Atom("b"), context));
        }

        [TestMethod]
        public void NoMatchAtomInteger()
        {
            Context context = new Context();

            Assert.IsFalse(MatchUtilities.MatchObjects(new Atom("a"), 123, context));
        }
    }
}
