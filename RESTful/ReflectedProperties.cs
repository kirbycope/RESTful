using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace RESTful
{
    class ReflectedProperties
    {
        public static PropertyInfo[] _Properties = null;
        public static PropertyInfo[] Properties
        {
            get
            {
                if ((_Properties == null) && (ReflectedType.Type != null))
                {
                    _Properties = ReflectedType.Type.GetProperties();
                }

                return _Properties;
            }
        }
    }
}
