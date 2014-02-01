namespace AjErl.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Language;

    [TestClass]
    public class MapTests
    {
        [TestMethod]
        public void CreateMap()
        {
            Map map = new Map(new object[] { new Atom("a"), new Atom("b") }, new object[] { 1, 2 });

            Assert.AreEqual(1, map.GetValue(new Atom("a")));
            Assert.AreEqual(2, map.GetValue(new Atom("b")));
        }
    }
}
