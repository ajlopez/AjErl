namespace AjErl.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Utilities;

    // Based on Aktores.Core.Communication.TypeSerializer
    public class TypeSerializer
    {
        private Type type;
        private IList<PropertyType> properties;

        public TypeSerializer(string fullname, IEnumerable<PropertyType> properties)
        {
            this.type = TypeUtilities.GetType(fullname);
            this.properties = new List<PropertyType>(properties);
        }

        public TypeSerializer(Type type)
        {
            this.type = type;
            this.properties = new List<PropertyType>();

            foreach (var pi in type.GetProperties())
            {
                var prop = new PropertyType();
                prop.Name = pi.Name;
                var tp = pi.PropertyType;

                if (tp == typeof(string))
                    prop.Type = Types.String;
                else if (tp == typeof(int))
                    prop.Type = Types.Integer;
                else if (tp == typeof(double))
                    prop.Type = Types.Double;
                else
                {
                    prop.Type = Types.Object;
                    prop.TypeName = tp.FullName;
                }

                this.properties.Add(prop);
            }
        }

        public string TypeName { get { return this.type.FullName; } }

        public IList<PropertyType> Properties { get { return this.properties; } }

        public void SerializeObject(object obj, OutputChannel channel)
        {
            foreach (var prop in this.properties)
                channel.Write(this.type.GetProperty(prop.Name).GetValue(obj, null));
        }

        public object DeserializerObject(InputChannel channel)
        {
            var obj = Activator.CreateInstance(this.type);

            foreach (var prop in this.properties)
                this.type.GetProperty(prop.Name).SetValue(obj, channel.Read(), null);

            return obj;
        }
    }
}
