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
            // Build string from x:Name="RequestBody"
            StringBuilder sb = new StringBuilder(requestBody);
            sb.Replace(System.Environment.NewLine, "");     // Remove NewLine characters
            sb.Replace("{", "");                            // Remove {
            sb.Replace("}", "");                            // Remove }
            sb.Replace("\"", "");                           // Remove "

            // Split the string in multiple strings at the comma delimter
            string[] splitString = sb.ToString().Split(',');

            // Create an empty dictionary variable
            Dictionary<string, string> dict = new Dictionary<string, string>();

            // Split name and value by splitting the strings at the colon delimiter
            foreach (string s in splitString)
            {
                // Split at the : character
                string[] st = s.Split(':');

                // Ensure the string still holds some value
                if ((st.ElementAt(0) != null) && (st.ElementAt(0) != "") && (st.ElementAt(0) != "null")
                 && (st.ElementAt(1) != null) && (st.ElementAt(1) != "") && st.ElementAt(1) != "null")
                {
                    // Add each name/value as a key/value pair in the dictionary
                    dict.Add(st.ElementAt(0), st.ElementAt(1));
                }
            }

            // Send the dictionary to the SetObjectProperties method
            object instance = SetObjectProperties(dict);

            return instance;
        }

        public static object FromXmlString(string requestBody)
        {
            // Build string from x:Name="RequestBody"
            StringBuilder sb = new StringBuilder(requestBody);
            sb.Replace(System.Environment.NewLine, "");             // Remove NewLine character
            sb.Replace("<" + ReflectedType.Type.Name + ">", "");    // Remove >
            sb.Replace("</" + ReflectedType.Type.Name + ">", "");   // Remove <

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
                // Split at the > character
                string[] st = s.Split('>');

                // Ensure the string still holds some value
                if ((st.ElementAt(0) != null) && (st.ElementAt(0) != "") && (st.ElementAt(0) != "null")
                 && (st.ElementAt(1) != null) && (st.ElementAt(1) != "") && st.ElementAt(1) != "null")
                {
                    // Add each name/value as a key/value pair in the dictionary
                    dict.Add(st.ElementAt(0), st.ElementAt(1));
                }
            }

            // Send the dictionary to the SetObjectProperties method
            object instance = SetObjectProperties(dict);

            return instance;
        }

        private static object SetObjectProperties(Dictionary<string, string> dict)
        {
            // Create a new instance of the reflected type
            object instance = Activator.CreateInstance(ReflectedType.Type);

            // Match reflected type properties with those in the dictionary
            foreach (PropertyInfo pi in ReflectedProperties.Properties)
            {
                // For each KeyValuePair in the dictionary...
                foreach (var p in dict)
                {
                    // If the PropertyInfo's Name matches the KeyValuePair's Key...
                    if (pi.Name == p.Key)
                    {
                        // If the property type of PropertyInfo is an enumeration... 
                        if (pi.PropertyType.IsEnum)
                        {
                            // Set the PropertyInfo value to its enumeration value
                            pi.SetValue(instance, Enum.ToObject(pi.PropertyType, Convert.ToInt32(p.Value)), null);
                        }
                        // If the property type of PropertyInfo is a nullable integer
                        else if (pi.PropertyType == typeof(int?))
                        {
                            // Set the PropertyInfo value to the Value of the KeyValuePair (cast as a nullable integer)
                            pi.SetValue(instance, Convert.ChangeType(p.Value, typeof(int)), null);
                        }
                        // If the proprty type of PropertyInfo is a byte
                        else if (pi.PropertyType == typeof(byte[]))
                        {
                            // Convert the string to a byte[]
                            byte[] bytes = Convert.FromBase64String(p.Value);

                            // Set the PropertyInfo value to the Value of the KeyValuePair (cast as an byte array)
                            pi.SetValue(instance, bytes, null);
                        }
                        // Otherwise...
                        else
                        {
                            // Set the PropertyInfo value to the Value of the KeyValuePair
                            pi.SetValue(instance, Convert.ChangeType(p.Value, pi.PropertyType), null);
                        }
                    }
                }
            }

            return instance;
        }
    }
}
