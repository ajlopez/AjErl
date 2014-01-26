namespace AjErl.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MailboxTests
    {
        [TestMethod]
        public void AddAndTakeMessage()
        {
            Mailbox box = new Mailbox();

            box.Add(1);

            Assert.AreEqual(1, box.Take());
        }

        [TestMethod]
        public void AddAndTakeTwoMessages()
        {
            Mailbox box = new Mailbox();

            box.Add(1);
            box.Add(2);

            Assert.AreEqual(1, box.Take());
            Assert.AreEqual(2, box.Take());
        }
    }
}
