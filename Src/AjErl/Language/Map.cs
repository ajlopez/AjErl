namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Map
    {
        private Map parent;
        private object[] keys;
        private object[] values;

        public Map(IList<object> keys, IList<object> values)
        {
            this.keys = keys.ToArray();
            this.values = values.ToArray();
            this.parent = null;
        }

        private Map(object[] keys, object[] values)
        {
            this.keys = keys;
            this.values = values;
            this.parent = null;
        }

        private Map(Map parent, object[] keys, object[] values)
        {
            this.keys = keys;
            this.values = values;
            this.parent = parent;
        }

        public object GetValue(object key)
        {
            int position = ((IList<object>)this.keys).IndexOf(key);

            if (position < 0)
                if (this.parent != null)
                    return this.parent.GetValue(key);
                else
                    throw new InvalidOperationException(string.Format("undefined key {0}", key));

            return this.values[position];
        }

        public Map SetKeyValues(IList<object> keys, IList<object> newvalues)
        {
            object[] newvals = new object[this.values.Length];
            Array.Copy(this.values, newvals, this.values.Length);

            for (int k = 0; k < keys.Count; k++)
            {
                object key = keys[k];
                int position = ((IList<object>)this.keys).IndexOf(key);

                if (position < 0)
                    throw new InvalidOperationException(string.Format("undefined key {0}", key));

                newvals[position] = newvalues[k];
            }

            return new Map(this.keys, newvals);
        }

        public Map SetNewKeyValues(IList<object> keys, IList<object> values)
        {
            foreach (var key in keys)
                if (((IList<object>)this.keys).IndexOf(key) >= 0)
                    throw new InvalidOperationException(string.Format("already defined key {0}", key));

            return new Map(this, keys.ToArray(), values.ToArray());
        }
    }
}
