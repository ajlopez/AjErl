namespace AjErl.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EmptyList
    {
        private static EmptyList instance = new EmptyList();

        private EmptyList()
        {
        }

        public static EmptyList Instance { get { return instance; } }

        public override string ToString()
        {
            return "[]";
        }
    }
}
