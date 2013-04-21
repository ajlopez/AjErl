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
    }
}
