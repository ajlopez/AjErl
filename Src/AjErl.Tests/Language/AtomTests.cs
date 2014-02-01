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

        [TestMethod]
        public void Equals()
        {
            Atom atom1 = new Atom("a");
            Atom atom2 = new Atom("b");
            Atom atom3 = new Atom("a");

            Assert.IsTrue(atom1.Equals(atom3));
            Assert.IsTrue(atom3.Equals(atom1));

            Assert.AreEqual(atom1.GetHashCode(), atom3.GetHashCode());

            Assert.IsFalse(atom1.Equals(null));
            Assert.IsFalse(atom1.Equals(123));
            Assert.IsFalse(atom1.Equals("foo"));
            Assert.IsFalse(atom1.Equals(atom2));
            Assert.IsFalse(atom2.Equals(atom1));
        }
    }
}

