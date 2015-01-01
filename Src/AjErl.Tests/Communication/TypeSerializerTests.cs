namespace AjErl.Tests.Communication
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using AjErl.Communication;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TypeSerializerTests
    {
        [TestMethod]
        public void GetPersonTypeFullName()
        {
            var serializer = new TypeSerializer(typeof(Person));

            Assert.AreEqual("AjErl.Tests.Person", serializer.TypeName);
        }

        [TestMethod]
        public void GetPersonProperties()
        {
            var serializer = new TypeSerializer(typeof(Person));

            var props = serializer.Properties;

            Assert.IsNotNull(props);
            Assert.AreEqual(3, props.Count);

            Assert.AreEqual("Id", props[0].Name);
            Assert.AreEqual(Types.Integer, props[0].Type);
            Assert.IsNull(props[0].TypeName);

            Assert.AreEqual("FirstName", props[1].Name);
            Assert.AreEqual(Types.String, props[1].Type);
            Assert.IsNull(props[1].TypeName);

            Assert.AreEqual("LastName", props[2].Name);
            Assert.AreEqual(Types.String, props[2].Type);
            Assert.IsNull(props[2].TypeName);
        }

        [TestMethod]
        public void SerializeDeserializeType()
        {
            var serializer = new TypeSerializer(typeof(Person));

            MemoryStream stream = new MemoryStream();
            OutputChannel output = new OutputChannel(new BinaryWriter(stream));
            output.Write(serializer);
            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            var result = channel.Read(true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TypeSerializer));

            var newserializer = (TypeSerializer)result;
            var props = newserializer.Properties;

            Assert.IsNotNull(props);
            Assert.AreEqual(3, props.Count);

            Assert.AreEqual("Id", props[0].Name);
            Assert.AreEqual(Types.Integer, props[0].Type);
            Assert.IsNull(props[0].TypeName);

            Assert.AreEqual("FirstName", props[1].Name);
            Assert.AreEqual(Types.String, props[1].Type);
            Assert.IsNull(props[1].TypeName);

            Assert.AreEqual("LastName", props[2].Name);
            Assert.AreEqual(Types.String, props[2].Type);
            Assert.IsNull(props[2].TypeName);
        }

        [TestMethod]
        public void SerializePersonObject()
        {
            var serializer = new TypeSerializer(typeof(Person));
            var person = new Person()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith"
            };

            MemoryStream stream = new MemoryStream();
            OutputChannel output = new OutputChannel(new BinaryWriter(stream));
            serializer.SerializeObject(person, output);
            stream.Seek(0, SeekOrigin.Begin);

            InputChannel channel = new InputChannel(new BinaryReader(stream));

            Assert.AreEqual(1, channel.Read());
            Assert.AreEqual("John", channel.Read());
            Assert.AreEqual("Smith", channel.Read());
        }

        [TestMethod]
        public void CreateSerializerByFullNameAndProperties()
        {
            IList<PropertyType> properties = new List<PropertyType>();

            properties.Add(new PropertyType()
            {
                Name = "Id",
                Type = Types.Integer
            });

            properties.Add(new PropertyType()
            {
                Name = "FirstName",
                Type = Types.String
            });

            var serializer = new TypeSerializer("AjErl.Tests.Person", properties);

            Assert.AreEqual("AjErl.Tests.Person", serializer.TypeName);

            var props = serializer.Properties;

            Assert.IsNotNull(props);
            Assert.AreEqual(2, props.Count);

            Assert.AreEqual("Id", props[0].Name);
            Assert.AreEqual(Types.Integer, props[0].Type);
            Assert.IsNull(props[0].TypeName);

            Assert.AreEqual("FirstName", props[1].Name);
            Assert.AreEqual(Types.String, props[1].Type);
            Assert.IsNull(props[1].TypeName);
        }
    }
}
