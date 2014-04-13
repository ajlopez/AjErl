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
