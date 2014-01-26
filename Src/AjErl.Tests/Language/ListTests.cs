namespace AjErl.Tests.Language
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            Assert.IsFalse(list.Match(null, context));
        }

        [TestMethod]
        public void MatchSameList()
        {
            List list = new List(1, 2);
            Context context = new Context();

            Assert.IsTrue(list.Match(list, context));
        }

        [TestMethod]
        public void MatchTwoLists()
        {
            List list = new List(1, 2);
            List list2 = new List(1, 2);
            Context context = new Context();

            Assert.IsTrue(list.Match(list2, context));
            Assert.IsTrue(list2.Match(list, context));
        }

        [TestMethod]
        public void NoMatchWithDifferentHeads()
        {
            List list = new List(0, 2);
            List list2 = new List(1, 2);
            Context context = new Context();

            Assert.IsFalse(list.Match(list2, context));
            Assert.IsFalse(list2.Match(list, context));
        }

        [TestMethod]
        public void NoMatchWithDifferentTails()
        {
            List list = new List(1, 0);
            List list2 = new List(1, 2);
            Context context = new Context();

            Assert.IsFalse(list.Match(list2, context));
            Assert.IsFalse(list2.Match(list, context));
        }

        [TestMethod]
        public void MatchTwoListsWithThreeElements()
        {
            List list = new List(1, new List(2, 3));
            List list2 = new List(1, new List(2, 3));
            Context context = new Context();

            Assert.IsTrue(list.Match(list2, context));
            Assert.IsTrue(list2.Match(list, context));
        }

        [TestMethod]
        public void NoMatchTwoListsWithDifferentLength()
        {
            List list = new List(1, 2);
            List list2 = new List(1, new List(2, 3));
            Context context = new Context();

            Assert.IsFalse(list.Match(list2, context));
            Assert.IsFalse(list2.Match(list, context));
        }

        [TestMethod]
        public void SimpleListToString()
        {
            List list = new List(1, 2);

            Assert.AreEqual("[1|2]", list.ToString());
        }

        [TestMethod]
        public void SimpleListWithTwoElementsToString()
        {
            List list = new List(1, new List(2, null));

            Assert.AreEqual("[1,2]", list.ToString());
        }

        [TestMethod]
        public void SimpleListWithTwoElementsAndTailToString()
        {
            List list = new List(1, new List(2, 3));

            Assert.AreEqual("[1,2|3]", list.ToString());
        }

        [TestMethod]
        public void MatchWithAVariable()
        {
            List list = new List(1, new Variable("X"));
            List list2 = new List(1, new List(2, 3));
            Context context = new Context();

            Assert.IsTrue(list.Match(list2, context));

            Assert.AreEqual(list2.Tail, context.GetValue("X"));

            Assert.IsTrue(list2.Match(list, context));

            Assert.AreEqual(list2.Tail, context.GetValue("X"));
        }

        [TestMethod]
        public void MatchHeadAndTailWithVariables()
        {
            List list = new List(new Variable("H"), new Variable("T"));
            List list2 = new List(1, new List(2, 3));
            Context context = new Context();

            Assert.IsTrue(list.Match(list2, context));

            Assert.AreEqual(list2.Head, context.GetValue("H"));
            Assert.AreEqual(list2.Tail, context.GetValue("T"));

            Assert.IsTrue(list2.Match(list, context));

            Assert.AreEqual(list2.Head, context.GetValue("H"));
            Assert.AreEqual(list2.Tail, context.GetValue("T"));
        }

        [TestMethod]
        public void FirstVariable()
        {
            List list = new List(new Variable("H"), new Variable("T"));
            var result = list.FirstVariable();

            Assert.IsNotNull(result);
            Assert.AreEqual("H", result.Name);
        }

        [TestMethod]
        public void FirstVariableInTail()
        {
            List list = new List(new Tuple(new object[] { new Atom("h") }), new Variable("T"));
            var result = list.FirstVariable();

            Assert.IsNotNull(result);
            Assert.AreEqual("T", result.Name);
        }

        [TestMethod]
        public void NoVariable()
        {
            List list = new List(1, 2);
            var result = list.FirstVariable();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void MakeSimpleList()
        {
            List list = List.MakeList(new object[] { 1, 2, 3 });

            Assert.IsNotNull(list);
            Assert.AreEqual("[1,2,3]", list.ToString());
        }

        [TestMethod]
        public void MakeSimpleListWithBooleans()
        {
            List list = List.MakeList(new object[] { false, true, false });

            Assert.IsNotNull(list);
            Assert.AreEqual("[false,true,false]", list.ToString());
        }

        [TestMethod]
        public void MakeSimpleListWithTail()
        {
            List list = List.MakeList(new object[] { 1, 2, 3 }, List.MakeList(new object[] { 4, 5 }));

            Assert.IsNotNull(list);
            Assert.AreEqual("[1,2,3,4,5]", list.ToString());
        }

        [TestMethod]
        public void MakeSimpleListWithVariableAsTail()
        {
            List list = List.MakeList(new object[] { 1, 2, 3 }, new Variable("X"));

            Assert.IsNotNull(list);
            Assert.AreEqual("[1,2,3|X]", list.ToString());
        }
    }
}

