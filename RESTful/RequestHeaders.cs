using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;

namespace RESTful
{
    class RequestHeaders
    {
        public static Dictionary<string, string> _RequestHeaders = null;
        public static Dictionary<string, string> GridToDictionary(Grid headerGrid)
        {
            // Build a dictionary for the Request Headers
            Dictionary<string, string> headers = new Dictionary<string, string>();

            // Get Current Header count
            int headerCount = headerGrid.RowDefinitions.Count;

            if ((headerCount > 0) && (RESTful.Properties.Settings.Default.RequestHeader != null) && (RESTful.Properties.Settings.Default.RequestHeader != ""))
            {
                for (int i = 0; i < headerCount; i++)
                {
                    // Get the UIElement (child of headerGrid) for {header} field from row i, column 0
                    UIElement childHeader = headerGrid.Children
                                .Cast<UIElement>()
                                .First(c => Grid.GetRow(c) == i && Grid.GetColumn(c) == 0);

                    // Cast the UIElement as a TextBox
                    TextBox headerHeader = (TextBox)childHeader;

                    // Get the UIElement (child of headerGrid) for {value} field from row i, column 1
                    UIElement childValue = headerGrid.Children
                                .Cast<UIElement>()
                                .First(c => Grid.GetRow(c) == i && Grid.GetColumn(c) == 1);

                    // Cast the UIElement as a TextBox
                    TextBox headerValue = (TextBox)childValue;

                    // Ensure blank rows are not added
                    if ((headerHeader.Text != null) & (headerHeader.Text != ""))
                    {
                        // Add header/value pairs to dictionary
                        headers.Add(headerHeader.Text, headerValue.Text);
                    }
                }
            }

            // Return the created dictionary
            return headers;
        }

        public static string DictToString(Dictionary<string, string> requestHeaders)
        {
            // Create a StingBuilder to hold 'stringified' dictionary
            StringBuilder sb = new StringBuilder();

            foreach(var r in requestHeaders)
            {
                sb.AppendLine(String.Format("{0}:{1}", r.Key, r.Value));
            }

            // Return the created string
            return sb.ToString();
        }

        public static Dictionary<string, string> StringToDict(string requestHeaders)
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
                    try
                    {
                        // Save to the dictionary
                        dict.Add(keyValues.ElementAt(0), keyValues.ElementAt(1));
                    }
                    catch
                    {
                        dict = null;
                    }
                }
            }

            return dict;
        }
    }
}
