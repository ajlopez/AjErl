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

        [TestMethod]
        public void MatchNull()
        {
            Tuple tuple = new Tuple(new object[] { 1, 2, 3 });
            Context context = new Context();
            Assert.IsNull(tuple.Match(null, context));
        }

        [TestMethod]
        public void MatchSameTuple()
        {
            Tuple tuple = new Tuple(new object[] { 1, 2, 3 });
            Context context = new Context();
            Assert.AreEqual(context, tuple.Match(tuple, context));
        }

        [TestMethod]
        public void MatchSimpleTuple()
        {
            Tuple tuple = new Tuple(new object[] { 1, 2, 3 });
            Tuple tuple2 = new Tuple(new object[] { 1, 2, 3 });
            Context context = new Context();
            Assert.AreEqual(context, tuple.Match(tuple2, context));
        }

        [TestMethod]
        public void MatchTuplesWithAVariable()
        {
            Tuple tuple = new Tuple(new object[] { 1, new Variable("X"), 3 });
            Tuple tuple2 = new Tuple(new object[] { 1, 2, 3 });
            Context context = new Context();
            Context result = tuple.Match(tuple2, context);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(context, result);
            Assert.AreEqual(2, result.GetValue("X"));

            Context result2 = tuple2.Match(tuple, context);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(context, result2);
            Assert.AreEqual(2, result2.GetValue("X"));
        }

        [TestMethod]
        public void MatchTuplesWithNullElement()
        {
            Tuple tuple = new Tuple(new object[] { 1, null, 3 });
            Tuple tuple2 = new Tuple(new object[] { 1, null, 3 });
            Context context = new Context();
            Assert.AreEqual(context, tuple.Match(tuple2, context));
        }

        [TestMethod]
        public void NoMatchTuplesWithNullElement()
        {
            Tuple tuple = new Tuple(new object[] { 1, 2, 3 });
            Tuple tuple2 = new Tuple(new object[] { 1, null, 3 });
            Context context = new Context();
            Assert.IsNull(tuple.Match(tuple2, context));
            Assert.IsNull(tuple2.Match(tuple, context));
        }

        [TestMethod]
        public void NoMatchTuplesWithDifferentArities()
        {
            Tuple tuple = new Tuple(new object[] { 1, 2, 3 });
            Tuple tuple2 = new Tuple(new object[] { 1, 2, 3, 4 });
            Context context = new Context();
            Assert.IsNull(tuple.Match(tuple2, context));
            Assert.IsNull(tuple2.Match(tuple, context));
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
        public void TupleWithAtomToString()
        {
            Tuple tuple = new Tuple(new object[] { 1, new Atom("atom"), 3 });

            Assert.AreEqual("{1, atom, 3}", tuple.ToString());
        }
    }
}
