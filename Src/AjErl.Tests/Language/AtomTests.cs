using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjErl.Language;

namespace AjErl.Tests.Language
{
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

