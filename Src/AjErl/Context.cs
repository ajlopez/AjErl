using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjErl
{
    public class Context
    {
        private IDictionary<string, object> values = new Dictionary<string, object>();
        private Context parent;

        public Context()
        {
        }

        public Context(Context parent)
        {
            this.parent = parent;
        }

        public void SetValue(string name, object value)
        {
            this.values[name] = value;
        }

        public object GetValue(string name)
        {
            return this.values[name];
        }

        public bool HasValue(string name)
        {
            if (this.values.ContainsKey(name))
                return true;

            if (this.parent != null)
                return this.parent.HasValue(name);

            return false;
        }
    }
}
