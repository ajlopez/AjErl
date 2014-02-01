namespace AjErl.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AjErl.Expressions;
    using AjErl.Language;

    public class ListsModule : Module
    {
        public ListsModule(Context context)
            : base(context)
        {
            this.SetName("lists");
            this.Context.SetValue("map/2", new FuncFunction(Map));
            this.Context.SetValue("filter/2", new FuncFunction(Filter));
            this.Context.SetValue("sum/1", new FuncFunction(Sum));
            this.Context.SetValue("all/2", new FuncFunction(All));
            this.Context.SetValue("any/2", new FuncFunction(Any));
            this.AddExportNames(new string[] { "map/2", "filter/2", "sum/1", "all/2", "any/2" });
        }

        private static object Map(Context context, IList<object> arguments)
        {
            IFunction function = (IFunction)arguments[0];
            List list = (List)arguments[1];
            IList<object> elements = new List<object>();

            while (list != null)
            {
                elements.Add(function.Apply(context, new object[] { list.Head }));
                list = (List)list.Tail;
            }

            return List.MakeList(elements);
        }

        private static object Filter(Context context, IList<object> arguments)
        {
            IFunction function = (IFunction)arguments[0];
            List list = (List)arguments[1];
            IList<object> elements = new List<object>();

            while (list != null)
            {
                var result = function.Apply(context, new object[] { list.Head });

                if (true.Equals(result))
                    elements.Add(list.Head);

                list = (List)list.Tail;
            }

            return List.MakeList(elements);
        }

        private static object All(Context context, IList<object> arguments)
        {
            if (arguments[1] is EmptyList)
                return true;

            IFunction function = (IFunction)arguments[0];
            List list = (List)arguments[1];

            while (list != null)
            {
                var result = function.Apply(context, new object[] { list.Head });

                if (false.Equals(result))
                    return false;

                list = (List)list.Tail;
            }

            return true;
        }

        private static object Any(Context context, IList<object> arguments)
        {
            if (arguments[1] is EmptyList)
                return false;

            IFunction function = (IFunction)arguments[0];
            List list = (List)arguments[1];

            while (list != null)
            {
                var result = function.Apply(context, new object[] { list.Head });

                if (true.Equals(result))
                    return true;

                list = (List)list.Tail;
            }

            return false;
        }

        private static object Sum(Context context, IList<object> arguments)
        {
            if (arguments[0] is EmptyList)
                return 0;

            AddExpression addexpr = new AddExpression(null, null);
            List list = (List)arguments[0];
            object result = 0;

            while (list != null)
            {
                result = addexpr.Apply(result, list.Head);
                list = (List)list.Tail;
            }

            return result;
        }
    }
}
