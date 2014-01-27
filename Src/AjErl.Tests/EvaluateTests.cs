namespace AjErl.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Compiler;
    using AjErl.Expressions;
    using AjErl.Forms;
    using AjErl.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;

    [TestClass]
    public class EvaluateTests
    {
        private Machine machine;
        private Context context;

        [TestInitialize]
        public void Setup()
        {
            this.machine = new Machine();
            this.context = this.machine.RootContext;
        }

        [TestMethod]
        public void EvaluateInteger()
        {
            Assert.AreEqual(1, this.EvaluateExpression("1."));
        }

        [TestMethod]
        public void EvaluateSum()
        {
            Assert.AreEqual(3, this.EvaluateExpression("1+2."));
        }

        [TestMethod]
        public void EvaluateVariableMatch()
        {
            Assert.AreEqual(3, this.EvaluateExpression("X=1+2."));
            Assert.AreEqual(3, this.context.GetValue("X"));
        }

        [TestMethod]
        public void EvaluateList()
        {
            var result = this.EvaluateExpression("[1,2,1+2].");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));
            Assert.AreEqual("[1,2,3]", result.ToString());
        }

        [TestMethod]
        public void EvaluateListWithTail()
        {
            var result = this.EvaluateExpression("[1,2|[3,4]].");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));
            Assert.AreEqual("[1,2,3,4]", result.ToString());
        }

        [TestMethod]
        public void EvaluateListWithExpressions()
        {
            var result = this.EvaluateExpression("[1+7,hello,2-2,{cost, apple, 30-20},3].");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));
            Assert.AreEqual("[8,hello,0,{cost,apple,10},3]", result.ToString());
        }

        [TestMethod]
        public void EvaluateListWithBoundVariableAsTail()
        {
            this.EvaluateExpression("ThingsToBuy = [{apples,10},{pears,6},{milk,3}].");
            var result = this.EvaluateExpression("ThingsToBuy1 = [{oranges,4},{newspaper,1}|ThingsToBuy].");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));
            Assert.AreEqual("[{oranges,4},{newspaper,1},{apples,10},{pears,6},{milk,3}]", result.ToString());
        }

        [TestMethod]
        public void EvaluateMathHeadTailToList()
        {
            this.EvaluateExpression("[Buy|ThingsToBuy] = [{oranges,4},{newspaper,1},{apples,10},{pears,6},{milk,3}].");

            Assert.AreEqual("{oranges,4}", this.context.GetValue("Buy").ToString());
            Assert.AreEqual("[{newspaper,1},{apples,10},{pears,6},{milk,3}]", this.context.GetValue("ThingsToBuy").ToString());
        }

        [TestMethod]
        public void EvaluateMathHeadMemberTailToList()
        {
            this.EvaluateExpression("[Buy1,Buy2|ThingsToBuy] = [{oranges,4},{newspaper,1},{apples,10},{pears,6},{milk,3}].");

            Assert.AreEqual("{oranges,4}", this.context.GetValue("Buy1").ToString());
            Assert.AreEqual("{newspaper,1}", this.context.GetValue("Buy2").ToString());
            Assert.AreEqual("[{apples,10},{pears,6},{milk,3}]", this.context.GetValue("ThingsToBuy").ToString());
        }

        [TestMethod]
        public void EvaluateUnboundVariable()
        {
            this.EvaluateWithError("X.", "variable 'X' is unbound");
        }

        [TestMethod]
        public void EvaluateNoMatch()
        {
            this.EvaluateWithError("{X,Y,X} = {{abc,12},42,true}.", "no match of right hand side value {{abc,12},42,true}");
            this.EvaluateWithError("X.", "variable 'X' is unboud");
            this.EvaluateWithError("Y.", "variable 'Y' is unboud");
        }

        [TestMethod]
        public void EvaluateMatch()
        {
            this.EvaluateTo("{X,Y,Z} = {{abc,12},42,true}.", "{{abc,12},42,true}");
            this.EvaluateTo("X.", "{abc,12}");
            this.EvaluateTo("Y.", "42");
            this.EvaluateTo("Z.", "true");
        }

        [TestMethod]
        public void EvaluateEmptyList()
        {
            this.EvaluateTo("[].", "[]");
        }

        [TestMethod]
        public void EvaluateSimpleForm()
        {
            var result = this.EvaluateForm("add(X,Y) -> X+Y.");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Function));
            Assert.AreSame(result, this.context.GetValue("add/2"));
        }

        [TestMethod]
        public void EvaluateAndCallSimpleForm()
        {
            this.EvaluateAndCallForm("add(X,Y) -> X+Y.", new object[] { 1, 2 }, 3);
        }

        [TestMethod]
        public void EvaluateAndCallForMultiForm()
        {
            var twice = this.EvaluateExpression("fun(X) -> X*2 end.");
            this.EvaluateAndCallForm("for(Max,Max,F) -> [F(Max)]; for(I,Max,F) -> [F(I)|for(I+1,Max,F)].", new object[] { 1, 3, twice}, List.MakeList(new object[] { 2, 4, 6 }));
        }

        [TestMethod]
        public void EvaluateAndCallSimpleFun()
        {
            this.EvaluateExpression("Add = fun(X,Y) -> X+Y end.");
            var result = this.EvaluateExpression("Add(1,2).");

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void EvaluateAndCallTimes()
        {
            this.EvaluateExpression("Mult = fun(Times) -> (fun(X) -> X * Times end) end.");
            this.EvaluateExpression("Triple = Mult(3).");
            var result = this.EvaluateExpression("Triple(4).");

            Assert.IsNotNull(result);
            Assert.AreEqual(12, result);
        }

        [TestMethod]
        public void EvaluateAndCallMultiFun()
        {
            this.EvaluateExpression("TempConvert = fun({c,C}) -> {f, 32 + C*9/5}; ({f,F}) -> {c, (F-32)*5/9} end.");
            var result = this.EvaluateExpression("TempConvert({c, 100}).");

            Assert.IsNotNull(result);
            Assert.AreEqual("{f,212}", result.ToString());

            result = this.EvaluateExpression("TempConvert({f, 212}).");

            Assert.IsNotNull(result);
            Assert.AreEqual("{c,100}", result.ToString());
        }

        [TestMethod]
        public void EvaluateStrictEqual()
        {
            Assert.AreEqual(true, this.EvaluateExpression("1 =:= 1."));
            Assert.AreEqual(true, this.EvaluateExpression("\"foo\" =:= \"foo\"."));
            Assert.AreEqual(false, this.EvaluateExpression("1 =:= 2."));
            Assert.AreEqual(false, this.EvaluateExpression("\"foo\" =:= \"bar\"."));
            Assert.AreEqual(false, this.EvaluateExpression("1 =:= 1.0."));
        }

        [TestMethod]
        public void EvaluateEqual()
        {
            Assert.AreEqual(true, this.EvaluateExpression("1 == 1."));
            Assert.AreEqual(true, this.EvaluateExpression("\"foo\" == \"foo\"."));
            Assert.AreEqual(false, this.EvaluateExpression("1 == 2."));
            Assert.AreEqual(false, this.EvaluateExpression("\"foo\" == \"bar\"."));
            Assert.AreEqual(true, this.EvaluateExpression("1 == 1.0."));
        }

        [TestMethod]
        public void EvaluateRem()
        {
            Assert.AreEqual(0, this.EvaluateExpression("4 rem 2."));
            Assert.AreEqual(1, this.EvaluateExpression("5 rem 2."));
        }

        [TestMethod]
        public void EvaluateBooleans()
        {
            Assert.AreEqual(false, this.EvaluateExpression("false."));
            Assert.AreEqual(true, this.EvaluateExpression("true."));
        }

        [TestMethod]
        public void EvaluateListsMap()
        {
            this.EvaluateTo("lists:map(fun(X) -> X*2 end, [1,2,3]).", "[2,4,6]");
            this.EvaluateTo("lists:map(fun(X) -> (X rem 2) =:= 0 end, [1,2,3]).", "[false,true,false]");
        }

        [TestMethod]
        public void EvaluateListsFilter()
        {
            this.EvaluateTo("lists:filter(fun(X) -> (X rem 2) =:= 0 end, [1,2,3,4,5]).", "[2,4]");
        }

        [TestMethod]
        public void EvaluateListsSum()
        {
            this.EvaluateTo("lists:sum([1,2,3,4]).", "10");
            this.EvaluateTo("lists:sum([1.2,3.4]).", "4.6");
            this.EvaluateTo("lists:sum([]).", "0");
        }

        [TestMethod]
        public void EvaluateListsAll()
        {
            this.EvaluateTo("lists:all(fun (X) -> (X rem 2) =:= 0 end, [1,2,3,4]).", "false");
            this.EvaluateTo("lists:all(fun (X) -> (X rem 2) =:= 0 end, [2,4]).", "true");
            this.EvaluateTo("lists:all(fun (X) -> (X rem 2) =:= 0 end, []).", "true");
        }

        [TestMethod]
        public void EvaluateListsAny()
        {
            this.EvaluateTo("lists:any(fun (X) -> (X rem 2) =:= 0 end, [1,2,3,4]).", "true");
            this.EvaluateTo("lists:any(fun (X) -> (X rem 2) =:= 0 end, [1,3]).", "false");
            this.EvaluateTo("lists:any(fun (X) -> (X rem 2) =:= 0 end, []).", "false");
        }

        [TestMethod]
        public void EvaluateListConstruct()
        {
            this.EvaluateTo("[1|[2,3]].", "[1,2,3]");
            this.EvaluateExpression("X=1.");
            this.EvaluateExpression("Y=[2,3,4].");
            this.EvaluateTo("[X|Y].", "[1,2,3,4]");
        }

        [TestMethod]
        public void EvaluateIoWrite()
        {
            StringWriter writer = new StringWriter();
            this.machine.TextWriter = writer;
            this.EvaluateTo("io:write(\"foo\").", "ok");
            Assert.AreEqual("foo", writer.ToString());
        }

        private void EvaluateWithError(string text, string message)
        {
            try
            {
                this.EvaluateExpression(text);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(message, ex.Message);
            }
        }

        private void EvaluateTo(string text, string value)
        {
            var result = this.EvaluateExpression(text);

            Assert.IsNotNull(result);
            Assert.AreEqual(value, Machine.ToString(result));
        }

        private void EvaluateAndCallForm(string text, IList<object> arguments, object expected)
        {
            var result = this.EvaluateForm(text);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IFunction));

            var func = (IFunction)result;

            Assert.AreEqual(expected, func.Apply(null, arguments));
        }

        private object EvaluateExpression(string text)
        {
            Parser parser = new Parser(text);
            IExpression expression = parser.ParseExpression();
            return expression.Evaluate(this.context);
        }

        private object EvaluateForm(string text)
        {
            Parser parser = new Parser(text);
            IForm form = parser.ParseForm();
            return form.Evaluate(this.context);
        }
    }
}
