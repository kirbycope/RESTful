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
                    // Get the selected value from x:Name="Types"
                    string selectedType = ((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue.ToString();

                    // Get the type
                    _ReflectedType = ReflectedAssembly.Assembly.GetType(selectedType);
                }

                return _ReflectedType;
            }
        }

    }
}
