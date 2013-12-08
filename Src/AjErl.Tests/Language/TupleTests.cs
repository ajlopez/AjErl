namespace AjErl.Tests.Language
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        [TestMethod]
        public void MatchNull()
        {
            Tuple tuple = new Tuple(new object[] { 1, 2, 3 });
            Context context = new Context();
            Assert.IsFalse(tuple.Match(null, context));
        }

        [TestMethod]
        public void MatchSameTuple()
        {
            Tuple tuple = new Tuple(new object[] { 1, 2, 3 });
            Context context = new Context();
            Assert.IsTrue(tuple.Match(tuple, context));
        }

        [TestMethod]
        public void MatchSimpleTuple()
        {
            Tuple tuple = new Tuple(new object[] { 1, 2, 3 });
            Tuple tuple2 = new Tuple(new object[] { 1, 2, 3 });
            Context context = new Context();
            Assert.IsTrue(tuple.Match(tuple2, context));
        }

        [TestMethod]
        public void MatchTuplesWithAVariable()
        {
            Tuple tuple = new Tuple(new object[] { 1, new Variable("X"), 3 });
            Tuple tuple2 = new Tuple(new object[] { 1, 2, 3 });
            Context context = new Context();
            Assert.IsTrue(tuple.Match(tuple2, context));
            Assert.AreEqual(2, context.GetValue("X"));

            Assert.IsTrue(tuple2.Match(tuple, context));

            Assert.AreEqual(2, context.GetValue("X"));
        }

        [TestMethod]
        public void MatchTuplesWithNullElement()
        {
            Tuple tuple = new Tuple(new object[] { 1, null, 3 });
            Tuple tuple2 = new Tuple(new object[] { 1, null, 3 });
            Context context = new Context();
            Assert.IsTrue(tuple.Match(tuple2, context));
        }

        [TestMethod]
        public void NoMatchTuplesWithNullElement()
        {
            Tuple tuple = new Tuple(new object[] { 1, 2, 3 });
            Tuple tuple2 = new Tuple(new object[] { 1, null, 3 });
            Context context = new Context();
            Assert.IsFalse(tuple.Match(tuple2, context));
            Assert.IsFalse(tuple2.Match(tuple, context));
        }

        [TestMethod]
        public void NoMatchTuplesWithDifferentArities()
        {
            Tuple tuple = new Tuple(new object[] { 1, 2, 3 });
            Tuple tuple2 = new Tuple(new object[] { 1, 2, 3, 4 });
            Context context = new Context();
            Assert.IsFalse(tuple.Match(tuple2, context));
            Assert.IsFalse(tuple2.Match(tuple, context));
        }

        [TestMethod]
        public void SimpleTupleToString()
        {
            Tuple tuple = new Tuple(new object[] { 1, 2, 3 });

            Assert.AreEqual("{1, 2, 3}", tuple.ToString());
        }

        [TestMethod]
        public void TupleWithVariableToString()
        {
            Tuple tuple = new Tuple(new object[] { 1, new Variable("X"), 3 });

            Assert.AreEqual("{1, X, 3}", tuple.ToString());
        }

        [TestMethod]
        public void FirstVariable()
        {
            Tuple tuple = new Tuple(new object[] { 1, new Variable("X"), 3 });
            var result = tuple.FirstVariable();

            Assert.IsNotNull(result);
            Assert.AreEqual("X", result.Name);
        }

        [TestMethod]
        public void FirstVariableWithNestedTuple()
        {
            Tuple tuple = new Tuple(new object[] { new Tuple(new object[] { 1, 2, 3}), new Variable("X"), 3 });
            var result = tuple.FirstVariable();

            Assert.IsNotNull(result);
            Assert.AreEqual("X", result.Name);
        }

        [TestMethod]
        public void NoVariable()
        {
            Tuple tuple = new Tuple(new object[] { 1, 2, 3 });
            var result = tuple.FirstVariable();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TupleWithAtomToString()
        {
            Tuple tuple = new Tuple(new object[] { 1, new Atom("atom"), 3 });

            Assert.AreEqual("{1, atom, 3}", tuple.ToString());
        }
    }
}
