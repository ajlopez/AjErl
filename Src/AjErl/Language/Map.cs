namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Map
    {
        private object[] keys;
        private object[] values;

        public Map(IList<object> keys, IList<object> values)
        {
            this.keys = keys.ToArray();
            this.values = values.ToArray();
        }

        private Map(object[] keys, object[] values)
        {
            this.keys = keys;
            this.values = values;
        }

        public object GetValue(object key)
        {
            int position = ((IList<object>)keys).IndexOf(key);

            if (position < 0)
                throw new InvalidOperationException(string.Format("undefined key {0}", key));

            return values[position];
        }

        public Map SetKeyValues(IList<object> keys, IList<object> newvalues)
        {
            object[] newvals = new object[this.values.Length];
            Array.Copy(this.values, newvals, this.values.Length);

            for (int k = 0; k < keys.Count; k++)
            {
                object key = keys[k];
                int position = ((IList<object>)this.keys).IndexOf(key);
                newvals[position] = newvalues[k];
            }

            return new Map(this.keys, newvals);
        }
    }
}
