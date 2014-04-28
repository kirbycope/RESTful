using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace RESTful
{
    class ObjectConverter
    {
        public static object FromJsonString(string requestBody)
        {
            StringBuilder sb = new StringBuilder(requestBody);
            sb.Replace(System.Environment.NewLine, "");
            sb.Replace("{", "");
            sb.Replace("}", "");
            sb.Replace("\"", "");

            // Split the string in multiple strings at the comma delimter
            string[] splitString = sb.ToString().Split(',');

            // Create an empty dictionary variable
            Dictionary<string, string> dict = new Dictionary<string, string>();

            // Split name and value by splitting the strings at the colon delimiter
            foreach (string s in splitString)
            {
                string[] st = s.Split(':');

                // Add each name/value as a key/value pair in the dictionary
                dict.Add(st.ElementAt(0), st.ElementAt(1));
            }

            // Create a new instance of the reflected type
            object instance = Activator.CreateInstance(ReflectedType.Type);

            // Match reflected type properties with those in the dictionary
            foreach (PropertyInfo pi in ReflectedProperties.Properties)
            {
                foreach (var p in dict)
                {
                    if (pi.Name == p.Key)
                    {
                        pi.SetValue(instance, Convert.ChangeType(p.Value, pi.PropertyType), null);
                    }
                }
            }

            return instance;
        }

        public static object FromXmlString(string requestBody)
        {
            StringBuilder sb = new StringBuilder(requestBody);
            sb.Replace(System.Environment.NewLine, "");
            sb.Replace("<" + ReflectedType.Type.Name + ">", "");
            sb.Replace("</" + ReflectedType.Type.Name + ">", "");

            // Remove end of nodes
            foreach (PropertyInfo pi in ReflectedProperties.Properties)
            {
                sb.Replace("</" + pi.Name + ">", "");
            }

            // Split the string in multiple strings at the less-than sign delimter
            string[] splitString = sb.ToString().Split('<');

            // Create an empty dictionary variable
            Dictionary<string, string> dict = new Dictionary<string, string>();

            // Split name and value by splitting the strings at the greater-than sign delimiter
            foreach (string s in splitString)
            {
                string[] st = s.Split('>');
                if ((st.ElementAt(0) != null) && (st.ElementAt(0) != "") && (st.ElementAt(1) != null) && (st.ElementAt(1) != null))
                {
                    // Add each name/value as a key/value pair in the dictionary
                    dict.Add(st.ElementAt(0), st.ElementAt(1));
                }
            }

            // Create a new instance of the reflected type
            object instance = Activator.CreateInstance(ReflectedType.Type);

            // Match reflected type properties with those in the dictionary
            foreach (PropertyInfo pi in ReflectedProperties.Properties)
            {
                foreach (var p in dict)
                {
                    if (pi.Name == p.Key)
                    {
                        pi.SetValue(instance, Convert.ChangeType(p.Value, pi.PropertyType), null);
                    }
                }
            }

            return instance;
        }
    }
}
