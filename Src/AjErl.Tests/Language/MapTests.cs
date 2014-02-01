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

        [TestMethod]
        public void SetKeyValues()
        {
            Map map = new Map(new object[] { new Atom("a"), new Atom("b"), new Atom("c") }, new object[] { 1, 2, 3 });

            Map newmap = map.SetKeyValues(new object[] { new Atom("c"), new Atom("a") }, new object[] { 4, 5 });

            Assert.AreEqual(1, map.GetValue(new Atom("a")));
            Assert.AreEqual(2, map.GetValue(new Atom("b")));
            Assert.AreEqual(3, map.GetValue(new Atom("c")));

            Assert.AreEqual(5, newmap.GetValue(new Atom("a")));
            Assert.AreEqual(2, newmap.GetValue(new Atom("b")));
            Assert.AreEqual(4, newmap.GetValue(new Atom("c")));
        }

        [TestMethod]
        public void RaiseIfUndefinedKey()
        {
            Map map = new Map(new object[] { new Atom("a"), new Atom("b") }, new object[] { 1, 2 });

            try
            {
                map.GetValue(new Atom("c"));
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("undefined key c", ex.Message);
            }
        }
    }
}
