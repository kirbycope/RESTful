using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace RESTful
{
    class Templates
    {
        public static string JSONTemplate()
        {
            // Get Properties
            PropertyInfo[] properties = ReflectedProperties.Properties;

            // Create a string to store the template
            StringBuilder sb = new StringBuilder();

            if (properties.Count() > 0)
            {
                // Begin JSON object
                sb.AppendLine("{");

                // Iterate through each property
                for (int i = 0; i < properties.Count(); i++)
                {
                    // Example: "Username":"String"
                    string property = String.Format(@"""{0}"":""{1}""", properties.ElementAt(i).Name, properties.ElementAt(i).PropertyType.Name);

                    // Add property to the string builder
                    sb.Append(property);

                    // Check if more properties follow
                    if (i < (properties.Count() - 1))
                    {
                        sb.AppendLine(",");
                    }
                    else
                    {
                        sb.AppendLine("");
                    }
                }

                // End JSON object
                sb.Append("}");
            }

            // Return the JSON Template as a string
            return sb.ToString();
        }

        public static string XMLTemplate()
        {
            // Get Properties
            PropertyInfo[] properties = ReflectedProperties.Properties;

            // Create a string to store the template
            StringBuilder sb = new StringBuilder();

            if (properties.Count() > 0)
            {
                // Begin XML object
                sb.AppendLine("<" + ReflectedType.Type.Name + ">");

                // Iterate through each property
                for (int i = 0; i < properties.Count(); i++)
                {
                    // Example: <Username>{0}</Username>    
                    string property = String.Format("<{0}>{1}</{0}>", properties.ElementAt(i).Name, properties.ElementAt(i).PropertyType.Name);

                    // Add property to the string builder
                    sb.AppendLine(property);
                }

                // End XML object
                sb.Append("</" + ReflectedType.Type.Name + ">");
            }

            // Return the XML Template as a string
            return sb.ToString();
        }
    }
}
