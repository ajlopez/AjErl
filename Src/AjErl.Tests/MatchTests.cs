namespace AjErl.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AjErl.Language;

    [TestClass]
    public class MatchTests
    {
        [TestMethod]
        public void MatchAnonymousVariable()
        {
            Context context = new Context();

            Assert.IsTrue(Match.MatchObjects(new Variable("_"), 1, context));

            Assert.IsNull(context.GetValue("_"));
        }

        [TestMethod]
        public void MatchVariable()
        {
            Context context = new Context();

            Assert.IsTrue(Match.MatchObjects(new Variable("X"), 1, context));

            Assert.AreEqual(1, context.GetValue("X"));
        }
    }
}
