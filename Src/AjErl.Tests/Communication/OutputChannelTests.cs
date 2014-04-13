namespace AjErl.Tests.Communication
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using AjErl.Communication;

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
            reader.Close();
        }
    }
}
