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
    }
}

