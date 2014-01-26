namespace AjErl.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AtomTests
    {
        [TestMethod]
        public void CreateSimpleAtom()
        {
            Atom atom = new Atom("one");

            Assert.AreEqual("one", atom.Name);
        }

        [TestMethod]
        public void AtomToString()
        {
            Atom atom = new Atom("one");

            Assert.AreEqual("one", atom.ToString());
        }

        [TestMethod]
        public void MatchWithNull()
        {
            Atom atom = new Atom("one");

            Assert.IsFalse(atom.Match(null));
        }

        [TestMethod]
        public void MatchWithAnotherAtom()
        {
            Atom atom = new Atom("one");
            Atom atom2 = new Atom("two");

            Assert.IsFalse(atom.Match(atom2));
        }
    }
}

