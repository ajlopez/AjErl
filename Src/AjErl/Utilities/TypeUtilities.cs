namespace AjErl.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    // Based on AjSharp AjLanguage.TypeUtilities
    // Based on PythonSharp.Utilities
    // Based on RubySharp.Utilities
    // Based on Aktores.Core.Utilities
    // For referenced assemblies check: http://stackoverflow.com/questions/9997508/referenced-assembly-not-found-how-to-get-all-dlls-included-in-solution
    public static class TypeUtilities
    {
        private static bool referencedAssembliesLoaded = false;

        public static Type GetType(string name)
        {
            Type type = Type.GetType(name);

            if (type != null)
                return type;

            type = GetTypeFromLoadedAssemblies(name);

            if (type != null)
                return type;

            type = GetTypeFromPartialNamedAssembly(name);

            if (type != null)
                return type;

            LoadReferencedAssemblies();

            type = GetTypeFromLoadedAssemblies(name);

            if (type != null)
                return type;

            return null;
        }

        private static Type GetTypeFromPartialNamedAssembly(string name)
        {
            int p = name.LastIndexOf(".");

            if (p < 0)
                return null;

            string assemblyName = name.Substring(0, p);

            try
            {
                Assembly assembly = Assembly.LoadWithPartialName(assemblyName);

                return assembly.GetType(name);
            }
            catch
            {
                return null;
            }
        }

        private static Type GetTypeFromLoadedAssemblies(string name)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type type = assembly.GetType(name);

                if (type != null)
                    return type;
            }

            return null;
        }

        private static void LoadReferencedAssemblies()
        {
            if (referencedAssembliesLoaded)
                return;

            List<string> loaded = new List<string>();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                loaded.Add(assembly.GetName().Name);

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                LoadReferencedAssemblies(assembly, loaded);

            referencedAssembliesLoaded = true;
        }

        private static void LoadReferencedAssemblies(Assembly assembly, List<string> loaded)
        {
            foreach (AssemblyName referenced in assembly.GetReferencedAssemblies())
                if (!loaded.Contains(referenced.Name))
                {
                    loaded.Add(referenced.Name);
                    Assembly newassembly = Assembly.Load(referenced);
                    LoadReferencedAssemblies(newassembly, loaded);
                }
        }
    }
}
