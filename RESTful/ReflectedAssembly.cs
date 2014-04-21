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
                if (_ReflectedAssembly == null)
                {
                    _ReflectedAssembly = Assembly.LoadFrom(RESTful.Properties.Settings.Default.ImportAssembly);
                }
                return _ReflectedAssembly;
            }
        }
    }

}
