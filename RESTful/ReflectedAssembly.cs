using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace RESTful
{
    class ReflectedAssembly
    {
        private static Assembly _ReflectedAssembly = null;
        public static Assembly Assembly
        {
            get
            {
                // Get the path from x:Name="AssemblyPath"
                string assemblyPath = ((MainWindow)System.Windows.Application.Current.MainWindow).AssemblyPath.Text;

                if ((_ReflectedAssembly == null) && (assemblyPath != null) && (assemblyPath != ""));
                {
                    // Load the imported assembly
                    _ReflectedAssembly = Assembly.LoadFrom(assemblyPath);
                }
                return _ReflectedAssembly;
            }
        }
    }

}