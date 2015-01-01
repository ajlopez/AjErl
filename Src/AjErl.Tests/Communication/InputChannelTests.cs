namespace AjErl.Tests.Communication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using AjErl.Communication;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InputChannelTests
    {
        [TestMethod]
        public void ReadNull()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel output = new OutputChannel(new BinaryWriter(stream));
            output.Write(null);
            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            Assert.IsNull(channel.Read());
        }

        [TestMethod]
        public void ReadInteger()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel output = new OutputChannel(new BinaryWriter(stream));
            output.Write(123);
            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            Assert.AreEqual(123, channel.Read());
        }

        [TestMethod]
        public void ReadShortInteger()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel output = new OutputChannel(new BinaryWriter(stream));
            short sh = short.MaxValue;
            output.Write(sh);
            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            Assert.AreEqual(sh, channel.Read());
        }

        [TestMethod]
        public void ReadLongInteger()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel output = new OutputChannel(new BinaryWriter(stream));
            long ln = long.MaxValue;
            output.Write(ln);
            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            Assert.AreEqual(ln, channel.Read());
        }

        [TestMethod]
        public void ReadCharacter()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel output = new OutputChannel(new BinaryWriter(stream));
            char ch = 'a';
            output.Write(ch);
            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            Assert.AreEqual(ch, channel.Read());
        }

        [TestMethod]
        public void ReadByte()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel output = new OutputChannel(new BinaryWriter(stream));
            byte bt = 64;
            output.Write(bt);
            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            Assert.AreEqual(bt, channel.Read());
        }

        [TestMethod]
        public void ReadDecimal()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel output = new OutputChannel(new BinaryWriter(stream));
            decimal dc = 12.34m;
            output.Write(dc);
            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            Assert.AreEqual(dc, channel.Read());
        }

        [TestMethod]
        public void ReadDouble()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel output = new OutputChannel(new BinaryWriter(stream));
            output.Write(123.45);
            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            Assert.AreEqual(123.45, channel.Read());
        }

        [TestMethod]
        public void ReadString()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel output = new OutputChannel(new BinaryWriter(stream));
            output.Write("foo");
            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            Assert.AreEqual("foo", channel.Read());
        }

        [TestMethod]
        public void WriteAndReadPersonObject()
        {
            var person = new Person()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith"
            };

            MemoryStream stream = new MemoryStream();
            OutputChannel output = new OutputChannel(new BinaryWriter(stream));
            output.Write(person);
            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            var result = channel.Read();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Person));

            var newperson = (Person)result;

            Assert.AreEqual(person.Id, newperson.Id);
            Assert.AreEqual(person.FirstName, newperson.FirstName);
            Assert.AreEqual(person.LastName, newperson.LastName);
        }

        [TestMethod]
        public void WriteAndReadTwoPersonObjects()
        {
            var person = new Person()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith"
            };

            var person2 = new Person()
            {
                Id = 2,
                FirstName = "Adam",
                LastName = "Pearson"
            };

            MemoryStream stream = new MemoryStream();
            OutputChannel output = new OutputChannel(new BinaryWriter(stream));
            output.Write(person);
            output.Write(person2);

            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            var result = channel.Read();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Person));

            var newperson = (Person)result;

            Assert.AreEqual(person.Id, newperson.Id);
            Assert.AreEqual(person.FirstName, newperson.FirstName);
            Assert.AreEqual(person.LastName, newperson.LastName);

            result = channel.Read();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Person));

            var newperson2 = (Person)result;

            Assert.AreEqual(person2.Id, newperson2.Id);
            Assert.AreEqual(person2.FirstName, newperson2.FirstName);
            Assert.AreEqual(person2.LastName, newperson2.LastName);
        }

        [TestMethod]
        public void ReadInvalidData()
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((byte)255);
            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            try
            {
                channel.Read();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidDataException));
            }
        }
    }
}
