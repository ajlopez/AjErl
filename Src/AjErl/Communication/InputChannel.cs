namespace AjErl.Communication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class InputChannel
    {
        private BinaryReader reader;
        private IList<TypeSerializer> serializers = new List<TypeSerializer>();

        public InputChannel(BinaryReader reader)
        {
            this.reader = reader;
        }

        public object Read(bool retserializer = false)
        {
            byte type = this.reader.ReadByte();

            switch (type)
            {
                case (byte)Types.Null:
                    return null;
                case (byte)Types.Integer:
                    return this.reader.ReadInt32();
                case (byte)Types.Double:
                    return this.reader.ReadDouble();
                case (byte)Types.String:
                    return this.reader.ReadString();
                case (byte)Types.Byte:
                    return this.reader.ReadByte();
                case (byte)Types.Character:
                    return this.reader.ReadChar();
                case (byte)Types.Single:
                    return this.reader.ReadSingle();
                case (byte)Types.Short:
                    return this.reader.ReadInt16();
                case (byte)Types.Long:
                    return this.reader.ReadInt64();
                case (byte)Types.Decimal:
                    return this.reader.ReadDecimal();
                case (byte)Types.Type:
                    var name = this.reader.ReadString();
                    var nprops = this.reader.ReadInt16();
                    IList<PropertyType> properties = new List<PropertyType>();

                    for (short k = 0; k < nprops; k++)
                    {
                        PropertyType property = new PropertyType();
                        property.Name = this.reader.ReadString();
                        property.Type = (Types)this.reader.ReadByte();

                        if (property.Type == Types.Object)
                            property.TypeName = this.reader.ReadString();

                        properties.Add(property);
                    }

                    var serializer = new TypeSerializer(name, properties);
                    this.serializers.Add(serializer);

                    if (retserializer)
                        return serializer;
                    else
                        return this.Read();
                case (byte)Types.Object:
                    var nserializer = this.reader.ReadInt16();
                    return this.serializers[nserializer].DeserializerObject(this);
            }

            throw new InvalidDataException();
        }
    }
}
