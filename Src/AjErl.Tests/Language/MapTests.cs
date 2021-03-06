﻿namespace AjErl.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void SetNewKeyValues()
        {
            Map map = new Map(new object[] { new Atom("a"), new Atom("b"), new Atom("c") }, new object[] { 1, 2, 3 });

            Map newmap = map.SetNewKeyValues(new object[] { new Atom("d"), new Atom("e") }, new object[] { 4, 5 });

            Assert.AreEqual(1, map.GetValue(new Atom("a")));
            Assert.AreEqual(2, map.GetValue(new Atom("b")));
            Assert.AreEqual(3, map.GetValue(new Atom("c")));

            Assert.AreEqual(1, newmap.GetValue(new Atom("a")));
            Assert.AreEqual(2, newmap.GetValue(new Atom("b")));
            Assert.AreEqual(3, newmap.GetValue(new Atom("c")));
            Assert.AreEqual(4, newmap.GetValue(new Atom("d")));
            Assert.AreEqual(5, newmap.GetValue(new Atom("e")));
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

        [TestMethod]
        public void RaiseIfSetUndefinedKey()
        {
            Map map = new Map(new object[] { new Atom("a"), new Atom("b"), new Atom("c") }, new object[] { 1, 2, 3 });

            try
            {
                map.SetKeyValues(new object[] { new Atom("d"), new Atom("a") }, new object[] { 4, 5 });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("undefined key d", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseIfSetAlreadyKey()
        {
            Map map = new Map(new object[] { new Atom("a"), new Atom("b"), new Atom("c") }, new object[] { 1, 2, 3 });

            try
            {
                map.SetNewKeyValues(new object[] { new Atom("d"), new Atom("a") }, new object[] { 4, 5 });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("already defined key a", ex.Message);
            }
        }
    }
}
