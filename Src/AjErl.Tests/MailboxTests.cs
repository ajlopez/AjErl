namespace AjErl.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
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

        [TestMethod]
        public void AddAndTakeMessagesWithReject()
        {
            Mailbox box = new Mailbox();

            box.Add(1);
            box.Add(2);
            box.Add(3);

            Assert.AreEqual(1, box.Take());
            Assert.AreEqual(2, box.Take());
            box.Reject(2);
            Assert.AreEqual(3, box.Take());
            Assert.AreEqual(2, box.Take());
        }

        [TestMethod]
        public void AddAndTakeMessagesWithTwoRejects()
        {
            Mailbox box = new Mailbox();

            box.Add(1);
            box.Add(2);
            box.Add(3);
            box.Add(4);

            Assert.AreEqual(1, box.Take());
            Assert.AreEqual(2, box.Take());
            box.Reject(2);
            Assert.AreEqual(3, box.Take());
            box.Reject(3);
            Assert.AreEqual(4, box.Take());
            Assert.AreEqual(2, box.Take());
            Assert.AreEqual(3, box.Take());
        }

        [TestMethod]
        public void AddAndTakeMessagesWithThreeNonConsecutiveRejects()
        {
            Mailbox box = new Mailbox();

            box.Add(1);
            box.Add(2);
            box.Add(3);
            box.Add(4);
            box.Add(5);

            Assert.AreEqual(1, box.Take());
            Assert.AreEqual(2, box.Take());
            box.Reject(2);
            Assert.AreEqual(3, box.Take());
            box.Reject(3);
            Assert.AreEqual(4, box.Take());
            Assert.AreEqual(2, box.Take());
            box.Reject(2);
            Assert.AreEqual(3, box.Take());
            Assert.AreEqual(2, box.Take());
            Assert.AreEqual(5, box.Take());
        }

        [TestMethod]
        public void TakeADelayedAddedMessage()
        {
            Mailbox box = new Mailbox();

            ThreadStart ts = new ThreadStart(() => { Thread.Sleep(100); box.Add(1); });
            Thread th = new Thread(ts);
            th.Start();

            Assert.AreEqual(1, box.Take());
        }
    }
}
