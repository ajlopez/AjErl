﻿namespace AjErl.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Compiler;
    using AjErl.Expressions;
    using AjErl.Language;

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
            Assert.AreEqual(1, this.Evaluate("1."));
        }

        [TestMethod]
        public void EvaluateSum()
        {
            Assert.AreEqual(3, this.Evaluate("1+2."));
        }

        [TestMethod]
        public void EvaluateVariableMatch()
        {
            Assert.AreEqual(3, this.Evaluate("X=1+2."));
            Assert.AreEqual(3, this.context.GetValue("X"));
        }

        [TestMethod]
        public void EvaluateList()
        {
            var result = this.Evaluate("[1,2,1+2].");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));
            Assert.AreEqual("[1,2,3]", result.ToString());
        }

        [TestMethod]
        public void EvaluateListWithTail()
        {
            var result = this.Evaluate("[1,2|[3,4]].");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));
            Assert.AreEqual("[1,2,3,4]", result.ToString());
        }

        [TestMethod]
        public void EvaluateListWithExpressions()
        {
            var result = this.Evaluate("[1+7,hello,2-2,{cost, apple, 30-20},3].");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));
            Assert.AreEqual("[8,hello,0,{cost,apple,10},3]", result.ToString());
        }

        [TestMethod]
        public void EvaluateListWithBoundVariableAsTail()
        {
            this.Evaluate("ThingsToBuy = [{apples,10},{pears,6},{milk,3}].");
            var result = this.Evaluate("ThingsToBuy1 = [{oranges,4},{newspaper,1}|ThingsToBuy].");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List));
            Assert.AreEqual("[{oranges,4},{newspaper,1},{apples,10},{pears,6},{milk,3}]", result.ToString());
        }

        [TestMethod]
        public void EvaluateMathHeadTailToList()
        {
            this.Evaluate("[Buy|ThingsToBuy] = [{oranges,4},{newspaper,1},{apples,10},{pears,6},{milk,3}].");

            Assert.AreEqual("{oranges,4}", this.context.GetValue("Buy").ToString());
            Assert.AreEqual("[{newspaper,1},{apples,10},{pears,6},{milk,3}]", this.context.GetValue("ThingsToBuy").ToString());
        }

        [TestMethod]
        public void EvaluateMathHeadMemberTailToList()
        {
            this.Evaluate("[Buy1,Buy2|ThingsToBuy] = [{oranges,4},{newspaper,1},{apples,10},{pears,6},{milk,3}].");

            Assert.AreEqual("{oranges,4}", this.context.GetValue("Buy1").ToString());
            Assert.AreEqual("{newspaper,1}", this.context.GetValue("Buy2").ToString());
            Assert.AreEqual("[{apples,10},{pears,6},{milk,3}]", this.context.GetValue("ThingsToBuy").ToString());
        }

        private object Evaluate(string text)
        {
            Parser parser = new Parser(text);
            IExpression expression = parser.ParseExpression();
            return expression.Evaluate(this.context);
        }
    }
}
