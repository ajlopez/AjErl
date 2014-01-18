namespace AjErl.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Compiler;
    using AjErl.Expressions;
    using AjErl.Forms;
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
        public void ThrowIfListIsNotClosed()
        {
            Parser parser = new Parser("[1,2");

            try
            {
                parser.ParseExpression();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("Expected ']'", ex.Message);
            }
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
        public void ParseSimpleDiv()
        {
            Parser parser = new Parser("10 div 20.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(DivExpression));

            DivExpression divexpression = (DivExpression)expression;

            Assert.IsInstanceOfType(divexpression.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(divexpression.RightExpression, typeof(ConstantExpression));

            Assert.AreEqual(10, ((ConstantExpression)divexpression.LeftExpression).Value);
            Assert.AreEqual(20, ((ConstantExpression)divexpression.RightExpression).Value);
        }

        [TestMethod]
        public void ParseSimpleRem()
        {
            Parser parser = new Parser("10 rem 20.");

            IExpression expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.IsInstanceOfType(expression, typeof(RemExpression));

            RemExpression remexpression = (RemExpression)expression;

            Assert.IsInstanceOfType(remexpression.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(remexpression.RightExpression, typeof(ConstantExpression));

            Assert.AreEqual(10, ((ConstantExpression)remexpression.LeftExpression).Value);
            Assert.AreEqual(20, ((ConstantExpression)remexpression.RightExpression).Value);
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

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSimpleCallExpression()
        {
            Parser parser = new Parser("add(1, 2).");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CallExpression));

            var expr = (CallExpression)result;

            Assert.IsInstanceOfType(expr.NameExpression, typeof(AtomExpression));
            Assert.AreEqual(((AtomExpression)expr.NameExpression).Atom.Name, "add");

            Assert.IsNotNull(expr.ArgumentExpressions);
            Assert.AreEqual(2, expr.ArgumentExpressions.Count);
            Assert.IsInstanceOfType(expr.ArgumentExpressions[0], typeof(ConstantExpression));
            Assert.IsInstanceOfType(expr.ArgumentExpressions[1], typeof(ConstantExpression));

            var cexpr = (ConstantExpression)expr.ArgumentExpressions[0];
            Assert.AreEqual(1, cexpr.Value);

            cexpr = (ConstantExpression)expr.ArgumentExpressions[1];
            Assert.AreEqual(2, cexpr.Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSimpleQualifiedCallExpression()
        {
            Parser parser = new Parser("mod:add(1, 2).");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(QualifiedCallExpression));

            var expr = (QualifiedCallExpression)result;

            Assert.IsInstanceOfType(expr.ModuleExpression, typeof(AtomExpression));
            Assert.AreEqual(((AtomExpression)expr.ModuleExpression).Atom.Name, "mod");

            Assert.IsInstanceOfType(expr.NameExpression, typeof(AtomExpression));
            Assert.AreEqual(((AtomExpression)expr.NameExpression).Atom.Name, "add");

            Assert.IsNotNull(expr.ArgumentExpressions);
            Assert.AreEqual(2, expr.ArgumentExpressions.Count);
            Assert.IsInstanceOfType(expr.ArgumentExpressions[0], typeof(ConstantExpression));
            Assert.IsInstanceOfType(expr.ArgumentExpressions[1], typeof(ConstantExpression));

            var cexpr = (ConstantExpression)expr.ArgumentExpressions[0];
            Assert.AreEqual(1, cexpr.Value);

            cexpr = (ConstantExpression)expr.ArgumentExpressions[1];
            Assert.AreEqual(2, cexpr.Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSimpleFunctionForm()
        {
            Parser parser = new Parser("one() -> 1.");

            var result = parser.ParseForm();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FunctionForm));

            var fdef = (FunctionForm)result;

            Assert.AreEqual("one", fdef.Name);
            Assert.IsNotNull(fdef.ParameterExpressions);
            Assert.AreEqual(0, fdef.ParameterExpressions.Count);
            Assert.IsNotNull(fdef.Body);
            Assert.IsInstanceOfType(fdef.Body, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)fdef.Body).Value);
        }

        [TestMethod]
        public void ParseSimpleFunctionFormWithVariableParameter()
        {
            Parser parser = new Parser("inc(X) -> X+1.");

            var result = parser.ParseForm();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FunctionForm));

            var fdef = (FunctionForm)result;

            Assert.AreEqual("inc", fdef.Name);
            Assert.IsNotNull(fdef.ParameterExpressions);
            Assert.AreEqual(1, fdef.ParameterExpressions.Count);
            Assert.IsInstanceOfType(fdef.ParameterExpressions[0], typeof(VariableExpression));
            Assert.AreEqual("X", ((VariableExpression)fdef.ParameterExpressions[0]).Variable.Name);
            Assert.IsNotNull(fdef.Body);
            Assert.IsInstanceOfType(fdef.Body, typeof(AddExpression));
        }

        [TestMethod]
        public void ParseEmptyStringAsNullForm()
        {
            Parser parser = new Parser(string.Empty);

            Assert.IsNull(parser.ParseForm());
        }

        [TestMethod]
        public void RaiseWhenUnexpectedIntegerParsingForm()
        {
            Parser parser = new Parser("123");

            try
            {
                parser.ParseForm();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("unexpected '123'", ex.Message);
            }
        }

        [TestMethod]
        public void ParseFunctionFormWithArgumentsAndExpressionBody()
        {
            Parser parser = new Parser("add(X,Y) -> X+Y.");

            var result = parser.ParseForm();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FunctionForm));

            var fdef = (FunctionForm)result;

            Assert.AreEqual("add", fdef.Name);
            Assert.IsNotNull(fdef.ParameterExpressions);

            Assert.AreEqual(2, fdef.ParameterExpressions.Count);
            Assert.IsInstanceOfType(fdef.ParameterExpressions[0], typeof(VariableExpression));
            Assert.IsInstanceOfType(fdef.ParameterExpressions[1], typeof(VariableExpression));
            Assert.AreEqual("X", ((VariableExpression)fdef.ParameterExpressions[0]).Variable.Name);
            Assert.AreEqual("Y", ((VariableExpression)fdef.ParameterExpressions[1]).Variable.Name);

            Assert.IsNotNull(fdef.Body);
            Assert.IsInstanceOfType(fdef.Body, typeof(AddExpression));

            var addexpr = (AddExpression)fdef.Body;
            Assert.IsInstanceOfType(addexpr.LeftExpression, typeof(VariableExpression));
            Assert.IsInstanceOfType(addexpr.RightExpression, typeof(VariableExpression));
            Assert.AreEqual("X", ((VariableExpression)addexpr.LeftExpression).Variable.Name);
            Assert.AreEqual("Y", ((VariableExpression)addexpr.RightExpression).Variable.Name);
        }

        [TestMethod]
        public void ParseMultiFunctionForm()
        {
            Parser parser = new Parser("f(0) -> 1; f(1) -> 2; f(X) -> f(X-1) + f(X-2).");

            var result = parser.ParseForm();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MultiFunctionForm));

            var fdef = (MultiFunctionForm)result;

            Assert.IsNotNull(fdef.Forms);
            Assert.AreEqual(3, fdef.Forms.Count);
        }

        [TestMethod]
        public void ParseSimpleModuleForm()
        {
            Parser parser = new Parser("-module(mymodule).");

            var result = parser.ParseForm();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ModuleForm));

            var mform = (ModuleForm)result;

            Assert.AreEqual("mymodule", mform.Name);
        }

        [TestMethod]
        public void RaiseIfNoNameInModuleForm()
        {
            Parser parser = new Parser("-module().");

            try
            {
                parser.ParseForm();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("Expected atom", ex.Message);
            }
        }
    }
}
