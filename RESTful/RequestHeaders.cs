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
        public static Dictionary<string, string> GridToHeaders()
        {
            // Get the HeadersGrid from the MainWindow
            Grid HeadersGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).HeadersGrid;

            // Create a dictionary to hold values
            Dictionary<string, string> dict = new Dictionary<string, string>();

            // Add Authentication headers
            if (((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationMethod.SelectedValue.ToString() == "BASIC")
            {
                // Get the encoded header
                KeyValuePair<string, string> kvp = BasicAuth.GetInputs();

                // Add authentication header to dictionary
                dict.Add(kvp.Key, kvp.Value);
            }
            else if (((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationMethod.SelectedValue.ToString() == "DIGEST")
            {
                // Get the encoded header
                KeyValuePair<string, string> kvp = DigestAuth.GetInputs();

                // Add authentication header to dictionary
                dict.Add(kvp.Key, kvp.Value);
            }
            else if (((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationMethod.SelectedValue.ToString() == "OAuth1")
            {
                // Get the encoded header
                KeyValuePair<string, string> kvp = OAuth1.GetInputs();
            }
            else if (((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationMethod.SelectedValue.ToString() == "OAuth2")
            {
                //
            }

            // Ensure there are headers in the HeadersGrid to be added
            if (HeadersGrid.Children.Count > 4)
            {
                 // Get each parameter starting after the first 3 labels/UIElements and go up to the last label/UIElement (plus sign)
                for (int i = 3; i < HeadersGrid.Children.Count - 1; i++)
                {
                    // Create stings for Key and Value of dictionary row
                    string headerKey = "";
                    string headerValue = "";

                    // Check if the UIElement is a TextBox
                    if (HeadersGrid.Children.Cast<UIElement>().ElementAt(i).GetType() == typeof(TextBox))
                    {
                        // Cast the child as a TextBox
                        TextBox uie = (TextBox)HeadersGrid.Children.Cast<UIElement>().ElementAt(i);

                        // Check if TextBox is a Key
                        if (uie.Name.Contains("Key"))
                        {
                            // Ensure the Text has value
                            if (uie.Text != "")
                            {
                                // Extract the Text value of the TextBox
                                headerKey = uie.Text;

                                // Get the Value for the key
                                if (HeadersGrid.Children.Cast<UIElement>().ElementAt(i + 1).GetType() == typeof(TextBox))
                                {
                                    // Cast the child as a TextBox
                                    TextBox uie2 = (TextBox)HeadersGrid.Children.Cast<UIElement>().ElementAt(i + 1);

                                    // Check if TextBox is a Key
                                    if (uie2.Name.Contains("Value"))
                                    {
                                        // Extract the Text value of the TextBox
                                        headerValue = uie2.Text;
                                    }
                                }
                            }
                        }
                    }
                    // Add row to dictionary
                    if ((headerKey != "") && (headerValue != "") && (dict.ContainsKey(headerKey) == false))
                    {
                        dict.Add(headerKey, headerValue);
                    }
                }
            }

            return dict;
        }
    }
}
