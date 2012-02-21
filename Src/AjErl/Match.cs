namespace AjErl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

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

            if (obj1 is Variable && !(obj2 is Variable))
            {
                Variable variable = (Variable) obj1;
                Context newcontext = new Context(context);
                newcontext.SetValue(variable.Name, obj2);
                return newcontext;
            }

            if (obj2 is Variable && !(obj1 is Variable))
            {
                Variable variable = (Variable)obj2;
                Context newcontext = new Context(context);
                newcontext.SetValue(variable.Name, obj1);
                return newcontext;
            }

            return null;
        }
    }
}
