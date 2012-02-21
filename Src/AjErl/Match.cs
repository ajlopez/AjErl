namespace AjErl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Match
    {
        public static Context MatchObjects(object obj1, object obj2, Context context)
        {
            if (obj1 == null)
                if (obj2 == null)
                    return context;
                else
                    return null;

            if (obj1.Equals(obj2))
                return context;

            return null;
        }
    }
}
