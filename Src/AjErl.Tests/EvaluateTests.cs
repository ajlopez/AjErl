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

    [TestClass]
    public class EvaluateTests
    {
        private Context context;

        [TestInitialize]
        public void Setup()
        {
            this.context = new Context();
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
            Assert.AreEqual(value, result.ToString());
        }

        private void EvaluateAndCallForm(string text, IList<object> arguments, object expected)
        {
            var result = this.EvaluateForm(text);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Function));

            var func = (Function)result;

            var newcontext = func.MakeContext(arguments);

            Assert.IsNotNull(newcontext);

            Assert.AreEqual(expected, func.Evaluate(newcontext));
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
