using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTful
{
    class RequestHeaders
    {
        public static Dictionary<string, string> List(string requestHeaders)
        {           
            // Split string by new lines
            string[] splitString = requestHeaders.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            // Create dictionary to hold header key/value pairs
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (string line in splitString)
            {
                // Split rows into key/ value pairs
                string[] keyValues = line.Split(':');

                if ((keyValues.ElementAt(0) != null) && (keyValues.ElementAt(0) != "") && (keyValues.ElementAt(1) != null) && (keyValues.ElementAt(1) != ""))
                {
                    // Save to the dictionary
                    dict.Add(keyValues.ElementAt(0), keyValues.ElementAt(1));
                }
            }

            return dict;
        }
    }
}
