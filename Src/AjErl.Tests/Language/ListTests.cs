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

        [TestMethod]
        public void NoMatchNullList()
        {
            List list = new List(1, 2);
            Context context = new Context();

            Assert.IsNull(list.Match(null, context));
        }

        [TestMethod]
        public void MatchSameList()
        {
            List list = new List(1, 2);
            Context context = new Context();

            Assert.AreEqual(context, list.Match(list, context));
        }

        [TestMethod]
        public void MatchTwoLists()
        {
            List list = new List(1, 2);
            List list2 = new List(1, 2);
            Context context = new Context();

            Assert.AreEqual(context, list.Match(list2, context));
            Assert.AreEqual(context, list2.Match(list, context));
        }

        [TestMethod]
        public void MatchTwoListsWithThreeElements()
        {
            List list = new List(1, new List(2, 3));
            List list2 = new List(1, new List(2, 3));
            Context context = new Context();

            Assert.AreEqual(context, list.Match(list2, context));
            Assert.AreEqual(context, list2.Match(list, context));
        }

        [TestMethod]
        public void NoMatchTwoListsWithDifferentLength()
        {
            List list = new List(1, 2);
            List list2 = new List(1, new List(2, 3));
            Context context = new Context();

            Assert.IsNull(list.Match(list2, context));
            Assert.IsNull(list2.Match(list, context));
        }
    }
}

