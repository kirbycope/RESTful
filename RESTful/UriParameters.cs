using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RESTful
{
    class UriParameters
    {
        public static string AddToURI()
        {
            // Get the ParametersGrid from the MainWindow
            Grid ParametersGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).HeadersGrid;

            // Create a string to hold the return value
            string URI = "";

            // Ensure there are parameters
            if (ParametersGrid.Children.Count > 4)
            {
                // Create a dictionary to hold values
                Dictionary<string, string> dict = new Dictionary<string, string>();

                // Get each parameter starting after the first 3 labels/UIElements and go up to the last label/UIElement (plus sign)
                for (int i = 3; i < ParametersGrid.Children.Count-1; i++)
                {
                    // Create stings for Key and Value of dictionary row
                    string parameterKey = "";
                    string parameterValue = "";

                    // Check if the UIElement is a TextBox
                    if (ParametersGrid.Children.Cast<UIElement>().ElementAt(i).GetType() == typeof(TextBox))
                    {
                        // Cast the child as a TextBox
                        TextBox uie = (TextBox)ParametersGrid.Children.Cast<UIElement>().ElementAt(i);

                        // Check if TextBox is a Key
                        if (uie.Name.Contains("Key"))
                        {
                            // Ensure the Text has value
                            if (uie.Text != "")
                            {
                                // Extract the Text value of the TextBox
                                parameterKey = uie.Text;

                                // Get the Value for the key
                                if (ParametersGrid.Children.Cast<UIElement>().ElementAt(i+1).GetType() == typeof(TextBox))
                                {
                                    // Cast the child as a TextBox
                                    TextBox uie2 = (TextBox)ParametersGrid.Children.Cast<UIElement>().ElementAt(i+1);

                                     // Check if TextBox is a Key
                                    if (uie2.Name.Contains("Value"))
                                    {
                                        // Extract the Text value of the TextBox
                                        parameterValue = uie2.Text;
                                    }
                                }
                            }
                        }
                    }

                    // Add row to dictionary
                    if ((parameterKey != "") && (parameterValue != "") && (dict.ContainsKey(parameterKey) == false))
                    {
                        dict.Add(parameterKey, parameterValue);
                    }
                }

                // Add items in dictionary to URI
                if (dict != null)
                {
                    // Get current URI
                    string uri = ((MainWindow)System.Windows.Application.Current.MainWindow).URI.Text;

                    StringBuilder sb = new StringBuilder(uri);
                    {
                        foreach (KeyValuePair<string, string> pair in dict)
                        {
                            // Make sure the same key isn't added twice
                            if (uri.Contains(pair.Key) == false)
                            {
                                // See if query string was started by the user
                                if (uri.Contains("?"))
                                {

                                    sb.Append(String.Format("&{0}={1}", pair.Key, pair.Value));
                                }
                                else
                                {
                                    sb.Append(String.Format("?{0}={1}", pair.Key, pair.Value));
                                }
                            }
                        }
                    }

                    // Update the URI
                    URI = sb.ToString();
                }
            }

            return URI;
        }
    }
}
