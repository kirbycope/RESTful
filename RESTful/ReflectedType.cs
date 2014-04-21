using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace RESTful
{
    class ReflectedType
    {
        public static Type _ReflectedType = null;
        public static Type Type
        {
            get
            {
                if ((_ReflectedType == null) && (ReflectedAssembly.Assembly != null))
                {
                    _ReflectedType = ReflectedAssembly.Assembly.GetType(RESTful.Properties.Settings.Default.Type);
                }

                return _ReflectedType;
            }
        }

    }
}
