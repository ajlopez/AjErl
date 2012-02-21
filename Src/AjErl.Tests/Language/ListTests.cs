using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AjErl.Language;

namespace AjErl.Tests.Language
{
    [TestClass]
    public class ListTests
    {
        [TestMethod]
        public void CreateSimpleList()
        {
            List list = new List(1, 2);

            Assert.AreEqual(1, list.Head);
            Assert.AreEqual(2, list.Tail);
        }
    }
}
