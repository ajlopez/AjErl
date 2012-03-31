﻿namespace AjErl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Language;

    public class Match
    {
        public static bool MatchObjects(object obj1, object obj2, Context context)
        {
            if (obj1 == null)
                if (obj2 == null)
                    return true;
                else
                    return false;

            if (obj1.Equals(obj2))
                return true;

            if (obj1 is Variable && !(obj2 is Variable))
            {
                Variable variable = (Variable) obj1;
                context.SetValue(variable.Name, obj2);
                return true;
            }

            if (obj2 is Variable && !(obj1 is Variable))
            {
                Variable variable = (Variable)obj2;
                context.SetValue(variable.Name, obj1);
                return true;
            }

            if (obj1 is List)
                if (obj2 is List)
                    return ((List)obj1).Match((List)obj2, context);
                else
                    return false;

            if (obj1 is Tuple)
                if (obj2 is Tuple)
                    return ((Tuple)obj1).Match((Tuple)obj2, context);
                else
                    return false;

            return false;
        }
    }
}