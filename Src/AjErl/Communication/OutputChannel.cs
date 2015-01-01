namespace AjErl.Communication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class OutputChannel
    {
        private BinaryWriter writer;
        private IList<TypeSerializer> serializers = new List<TypeSerializer>();
        private IDictionary<string, int> typepositions = new Dictionary<string, int>();

        public OutputChannel(BinaryWriter writer)
        {
            this.writer = writer;
        }

        public void Write(object obj)
        {
            if (obj == null)
            {
                this.writer.Write((byte)Types.Null);
                return;
            }

            if (obj is int)
            {
                this.writer.Write((byte)Types.Integer);
                this.writer.Write((int)obj);
                return;
            }

            if (obj is short)
            {
                this.writer.Write((byte)Types.Short);
                this.writer.Write((short)obj);
                return;
            }

            if (obj is long)
            {
                this.writer.Write((byte)Types.Long);
                this.writer.Write((long)obj);
                return;
            }

            if (obj is char)
            {
                this.writer.Write((byte)Types.Character);
                this.writer.Write((char)obj);
                return;
            }

            if (obj is byte)
            {
                this.writer.Write((byte)Types.Byte);
                this.writer.Write((byte)obj);
                return;
            }

            if (obj is double)
            {
                this.writer.Write((byte)Types.Double);
                this.writer.Write((double)obj);
                return;
            }

            if (obj is float)
            {
                this.writer.Write((byte)Types.Single);
                this.writer.Write((float)obj);
                return;
            }

            if (obj is string)
            {
                this.writer.Write((byte)Types.String);
                this.writer.Write((string)obj);
                return;
            }

            if (obj is decimal)
            {
                this.writer.Write((byte)Types.Decimal);
                this.writer.Write((decimal)obj);
                return;
            }

            if (obj is TypeSerializer)
            {
                var ts = (TypeSerializer)obj;
                var props = ts.Properties;
                this.writer.Write((byte)Types.Type);
                this.writer.Write(ts.TypeName);
                this.writer.Write((short)props.Count);

                foreach (var prop in props)
                {
                    this.writer.Write(prop.Name);
                    this.writer.Write((byte)prop.Type);

                    if (prop.Type == Types.Object)
                        this.writer.Write(prop.TypeName);
                }

                return;
            }

            var type = obj.GetType();
            string typename = type.FullName;

            TypeSerializer serializer;

            if (!this.typepositions.ContainsKey(typename))
            {
                serializer = new TypeSerializer(type);
                this.typepositions[typename] = this.serializers.Count;
                this.serializers.Add(serializer);
                this.Write(serializer);
            }
            else
                serializer = this.serializers[this.typepositions[typename]];

            this.writer.Write((byte)Types.Object);
            this.writer.Write((short)this.typepositions[typename]);

            serializer.SerializeObject(obj, this);
        }
    }
}
