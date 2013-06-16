namespace AjErl.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Compiler;
    using AjErl.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseVariable()
        {
            Parser parser = new Parser("X.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(VariableExpression));

            VariableExpression varexpression = (VariableExpression)expression;
            Assert.AreEqual("X", varexpression.Variable.Name);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseAtom()
        {
            Parser parser = new Parser("ok.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(AtomExpression));

            AtomExpression atomexpression = (AtomExpression)expression;
            Assert.AreEqual("ok", atomexpression.Atom.Name);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSimpleMatch()
        {
            Parser parser = new Parser("X=ok.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(MatchExpression));

            MatchExpression matchexpression = (MatchExpression)expression;
            Assert.IsNotNull(matchexpression.LeftExpression);
            Assert.IsNotNull(matchexpression.RightExpression);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseInteger()
        {
            Parser parser = new Parser("123.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));

            ConstantExpression consexpression = (ConstantExpression)expression;
            Assert.IsNotNull(consexpression.Value);
            Assert.AreEqual(123, consexpression.Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseString()
        {
            Parser parser = new Parser("\"foo\".");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));

            ConstantExpression consexpression = (ConstantExpression)expression;
            Assert.IsNotNull(consexpression.Value);
            Assert.AreEqual("foo", consexpression.Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseTuple()
        {
            Parser parser = new Parser("{1,2,3}.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(TupleExpression));

            TupleExpression tupleexpression = (TupleExpression)expression;
            Assert.IsNotNull(tupleexpression.Expressions);
            Assert.AreEqual(3, tupleexpression.Expressions.Count);

            foreach (var expr in tupleexpression.Expressions)
                Assert.IsInstanceOfType(expr, typeof(ConstantExpression));

            Assert.IsNull(parser.ParseExpression());
        }
    }
}
