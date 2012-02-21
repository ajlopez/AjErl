using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjErl.Language;

namespace AjErl.Tests.Language
{
    [TestClass]
    public class TupleTests
    {
        [TestMethod]
        public void CreateSimpleTuple()
        {
            Tuple tuple = new Tuple(new object[] { 1, 2, 3 });

            Assert.AreEqual(3, tuple.Arity);
            Assert.AreEqual(1, tuple.ElementAt(0));
            Assert.AreEqual(2, tuple.ElementAt(1));
            Assert.AreEqual(3, tuple.ElementAt(2));
        }
    }
}
