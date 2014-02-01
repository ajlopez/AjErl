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

        public object GetValue(object key)
        {
            int position = ((IList<object>)keys).IndexOf(key);

            return values[position];
        }
    }
}
