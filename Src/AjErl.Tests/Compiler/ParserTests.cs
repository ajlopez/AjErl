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
        public void ParseReal()
        {
            Parser parser = new Parser("123.45.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ConstantExpression));

            ConstantExpression consexpression = (ConstantExpression)expression;
            Assert.IsNotNull(consexpression.Value);
            Assert.AreEqual(123.45, consexpression.Value);

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
        public void ParseList()
        {
            Parser parser = new Parser("[1,2,3].");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(ListExpression));

            ListExpression listexpression = (ListExpression)expression;
            Assert.IsNotNull(listexpression.Expressions);
            Assert.AreEqual(3, listexpression.Expressions.Count);

            foreach (var expr in listexpression.Expressions)
                Assert.IsInstanceOfType(expr, typeof(ConstantExpression));

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

        [TestMethod]
        public void ParseEmptyTuple()
        {
            Parser parser = new Parser("{}.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(TupleExpression));

            TupleExpression tupleexpression = (TupleExpression)expression;
            Assert.IsNotNull(tupleexpression.Expressions);
            Assert.AreEqual(0, tupleexpression.Expressions.Count);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseUnaryTuple()
        {
            Parser parser = new Parser("{1}.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(TupleExpression));

            TupleExpression tupleexpression = (TupleExpression)expression;
            Assert.IsNotNull(tupleexpression.Expressions);
            Assert.AreEqual(1, tupleexpression.Expressions.Count);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseMatchVariableWithInteger()
        {
            Parser parser = new Parser("X=1.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(MatchExpression));

            MatchExpression matchexpression = (MatchExpression)expression;
            Assert.IsNotNull(matchexpression.LeftExpression);
            Assert.IsInstanceOfType(matchexpression.LeftExpression, typeof(VariableExpression));
            Assert.IsNotNull(matchexpression.RightExpression);
            Assert.IsInstanceOfType(matchexpression.RightExpression, typeof(ConstantExpression));

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ThrowIfUnexpectedComma()
        {
            Parser parser = new Parser(",");

            try
            {
                parser.ParseExpression();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("Unexpected ','", ex.Message);
            }
        }

        [TestMethod]
        public void ThrowIfNoPoint()
        {
            Parser parser = new Parser("1");

            try
            {
                parser.ParseExpression();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("Expected '.'", ex.Message);
            }
        }

        [TestMethod]
        public void ThrowIfTupleIsNotClosed()
        {
            Parser parser = new Parser("{1,2");

            try
            {
                parser.ParseExpression();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("Expected '}'", ex.Message);
            }
        }

        [TestMethod]
        public void ThrowIfTupleHasUnexpectedPoint()
        {
            Parser parser = new Parser("{1,2,.");

            try
            {
                parser.ParseExpression();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("Unexpected '.'", ex.Message);
            }
        }

        [TestMethod]
        public void ParseSimpleAdd()
        {
            Parser parser = new Parser("10+20.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(AddExpression));

            AddExpression addexpression = (AddExpression)expression;

            Assert.IsInstanceOfType(addexpression.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(addexpression.RightExpression, typeof(ConstantExpression));

            Assert.AreEqual(10, ((ConstantExpression)addexpression.LeftExpression).Value);
            Assert.AreEqual(20, ((ConstantExpression)addexpression.RightExpression).Value);
        }

        [TestMethod]
        public void ParseSimpleSubtract()
        {
            Parser parser = new Parser("10-20.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(SubtractExpression));

            SubtractExpression subtractexpression = (SubtractExpression)expression;

            Assert.IsInstanceOfType(subtractexpression.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(subtractexpression.RightExpression, typeof(ConstantExpression));

            Assert.AreEqual(10, ((ConstantExpression)subtractexpression.LeftExpression).Value);
            Assert.AreEqual(20, ((ConstantExpression)subtractexpression.RightExpression).Value);
        }

        [TestMethod]
        public void ParseSimpleMultiply()
        {
            Parser parser = new Parser("10*20.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(MultiplyExpression));

            MultiplyExpression multiplyexpression = (MultiplyExpression)expression;

            Assert.IsInstanceOfType(multiplyexpression.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(multiplyexpression.RightExpression, typeof(ConstantExpression));

            Assert.AreEqual(10, ((ConstantExpression)multiplyexpression.LeftExpression).Value);
            Assert.AreEqual(20, ((ConstantExpression)multiplyexpression.RightExpression).Value);
        }

        [TestMethod]
        public void ParseSimpleDivide()
        {
            Parser parser = new Parser("10/20.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(DivideExpression));

            DivideExpression divideexpression = (DivideExpression)expression;

            Assert.IsInstanceOfType(divideexpression.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(divideexpression.RightExpression, typeof(ConstantExpression));

            Assert.AreEqual(10, ((ConstantExpression)divideexpression.LeftExpression).Value);
            Assert.AreEqual(20, ((ConstantExpression)divideexpression.RightExpression).Value);
        }

        [TestMethod]
        public void ParseAddMultiply()
        {
            Parser parser = new Parser("2+3*4.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(AddExpression));

            AddExpression addexpression = (AddExpression)expression;

            Assert.IsInstanceOfType(addexpression.LeftExpression, typeof(ConstantExpression));
            Assert.AreEqual(2, ((ConstantExpression)addexpression.LeftExpression).Value);

            Assert.IsInstanceOfType(addexpression.RightExpression, typeof(MultiplyExpression));

            MultiplyExpression multiplyexpression = (MultiplyExpression)addexpression.RightExpression;

            Assert.IsInstanceOfType(multiplyexpression.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(multiplyexpression.RightExpression, typeof(ConstantExpression));

            Assert.AreEqual(3, ((ConstantExpression)multiplyexpression.LeftExpression).Value);
            Assert.AreEqual(4, ((ConstantExpression)multiplyexpression.RightExpression).Value);
        }

        [TestMethod]
        public void ParseAddMultiplyWithParens()
        {
            Parser parser = new Parser("(2+3)*4.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(MultiplyExpression));

            MultiplyExpression multexpression = (MultiplyExpression)expression;

            Assert.IsInstanceOfType(multexpression.RightExpression, typeof(ConstantExpression));
            Assert.AreEqual(4, ((ConstantExpression)multexpression.RightExpression).Value);

            Assert.IsInstanceOfType(multexpression.LeftExpression, typeof(AddExpression));

            AddExpression addexpression = (AddExpression)multexpression.LeftExpression;

            Assert.IsInstanceOfType(addexpression.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(addexpression.RightExpression, typeof(ConstantExpression));

            Assert.AreEqual(2, ((ConstantExpression)addexpression.LeftExpression).Value);
            Assert.AreEqual(3, ((ConstantExpression)addexpression.RightExpression).Value);
        }
    }
}
