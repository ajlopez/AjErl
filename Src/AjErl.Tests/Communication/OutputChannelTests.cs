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
    public class OutputChannelTests
    {
        [TestMethod]
        public void WriteInteger()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel channel = new OutputChannel(new BinaryWriter(stream));

            channel.Write(123);

            stream.Seek(0, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(stream);

            Assert.AreEqual((byte)Types.Integer, reader.ReadByte());
            Assert.AreEqual(123, reader.ReadInt32());
            Assert.AreEqual(-1, reader.PeekChar());

            reader.Close();
        }

        [TestMethod]
        public void WriteShortInteger()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel channel = new OutputChannel(new BinaryWriter(stream));

            short sh = 123;

            channel.Write(sh);

            stream.Seek(0, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(stream);

            Assert.AreEqual((byte)Types.Short, reader.ReadByte());
            Assert.AreEqual(sh, reader.ReadInt16());
            Assert.AreEqual(-1, reader.PeekChar());

            reader.Close();
        }

        [TestMethod]
        public void WriteLongInteger()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel channel = new OutputChannel(new BinaryWriter(stream));

            long ln = 123;

            channel.Write(ln);

            stream.Seek(0, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(stream);

            Assert.AreEqual((byte)Types.Long, reader.ReadByte());
            Assert.AreEqual(ln, reader.ReadInt64());
            Assert.AreEqual(-1, reader.PeekChar());

            reader.Close();
        }

        [TestMethod]
        public void WriteByte()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel channel = new OutputChannel(new BinaryWriter(stream));

            byte bt = 64;

            channel.Write(bt);

            stream.Seek(0, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(stream);

            Assert.AreEqual((byte)Types.Byte, reader.ReadByte());
            Assert.AreEqual(bt, reader.ReadByte());
            Assert.AreEqual(-1, reader.PeekChar());

            reader.Close();
        }

        [TestMethod]
        public void WriteDecimal()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel channel = new OutputChannel(new BinaryWriter(stream));

            decimal dc = 12.34m;

            channel.Write(dc);

            stream.Seek(0, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(stream);

            Assert.AreEqual((byte)Types.Decimal, reader.ReadByte());
            Assert.AreEqual(dc, reader.ReadDecimal());
            Assert.AreEqual(-1, reader.PeekChar());

            reader.Close();
        }

        [TestMethod]
        public void WriteChar()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel channel = new OutputChannel(new BinaryWriter(stream));

            char ch = 'a';

            channel.Write(ch);

            stream.Seek(0, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(stream);

            Assert.AreEqual((byte)Types.Character, reader.ReadByte());
            Assert.AreEqual(ch, reader.ReadChar());
            Assert.AreEqual(-1, reader.PeekChar());

            reader.Close();
        }

        [TestMethod]
        public void WriteString()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel channel = new OutputChannel(new BinaryWriter(stream));

            channel.Write("foo");

            stream.Seek(0, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(stream);

            Assert.AreEqual((byte)Types.String, reader.ReadByte());
            Assert.AreEqual("foo", reader.ReadString());
            Assert.AreEqual(-1, reader.PeekChar());

            reader.Close();
        }

        [TestMethod]
        public void WriteDouble()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel channel = new OutputChannel(new BinaryWriter(stream));

            channel.Write(123.45);

            stream.Seek(0, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(stream);

            Assert.AreEqual((byte)Types.Double, reader.ReadByte());
            Assert.AreEqual(123.45, reader.ReadDouble());
            Assert.AreEqual(-1, reader.PeekChar());

            reader.Close();
        }

        [TestMethod]
        public void WriteFloat()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel channel = new OutputChannel(new BinaryWriter(stream));

            channel.Write((float)123.45);

            stream.Seek(0, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(stream);

            Assert.AreEqual((byte)Types.Single, reader.ReadByte());
            Assert.AreEqual((float)123.45, reader.ReadSingle());
            Assert.AreEqual(-1, reader.PeekChar());

            reader.Close();
        }

        [TestMethod]
        public void WriteNull()
        {
            MemoryStream stream = new MemoryStream();
            OutputChannel channel = new OutputChannel(new BinaryWriter(stream));

            channel.Write(null);

            stream.Seek(0, SeekOrigin.Begin);

            BinaryReader reader = new BinaryReader(stream);

            Assert.AreEqual((byte)Types.Null, reader.ReadByte());
            Assert.AreEqual(-1, reader.PeekChar());

            reader.Close();
        }
    }
}
