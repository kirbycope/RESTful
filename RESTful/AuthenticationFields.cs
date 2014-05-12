using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RESTful
{
    class AuthenticationFields
    {
        public static void GenerateFields()
        {
            // Create empty string to hold the selected value
            string authenticationMethod = null;

            if (((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationMethod.SelectedValue != null)
            {
                // Get the selected authentication method
                authenticationMethod = ((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationMethod.SelectedValue.ToString();
            }

            if ((authenticationMethod != null) && (authenticationMethod != ""))
            {
                if (authenticationMethod == "Basic")
                {
                    // Generate fields
                    BasicAuth.GenereateFields();
                }
                else if (authenticationMethod == "Digest")
                {
                    // Generate fields
                    DigestAuth.GenereateFields();
                }
                else if (authenticationMethod == "OAuth1")
                {
                    // Generate fields
                    OAuth1.GenereateFields();
                }
                else if (authenticationMethod == "OAuth2")
                {
                    // Generate fields
                    OAuth2.GenereateFields();
                }
            }
        }

        public static AuthenticationDataElement[] GridToAuthenticationDataElement()
        {
            // Get the AuthenticationGrid from the MainWindow
            Grid AuthenticationGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationGrid;

            // Ensure there are fields
            if (AuthenticationGrid.Children.Count > 2)
            {
                // Create a dictionary to hold values
                Dictionary<string, string> dict = new Dictionary<string, string>();

                // Get each parameter starting after the first 2 UIElements
                for (int i = 2; i < AuthenticationGrid.Children.Count; i++)
                {
                    // Create stings for Key and Value of dictionary row
                    string fieldKey = "";
                    string fieldValue = "";

                    // Check if the UIElement is a Label
                    if (AuthenticationGrid.Children.Cast<UIElement>().ElementAt(i).GetType() == typeof(Label))
                    {
                        // Cast the child as a Label
                        Label uie = (Label)AuthenticationGrid.Children.Cast<UIElement>().ElementAt(i);

                        // Extract the Content value of the Label
                        fieldKey = uie.Content.ToString();

                        // Remove ':' from label
                        fieldKey.Replace(":", "");

                        // Check if the next UIElement is a TextBox
                        if (AuthenticationGrid.Children.Cast<UIElement>().ElementAt(i + 1).GetType() == typeof(TextBox))
                        {
                            // Cast the child as a TextBox
                            TextBox uie2 = (TextBox)AuthenticationGrid.Children.Cast<UIElement>().ElementAt(i + 1);

                            // Extract the Text value of the TextBox
                            fieldValue = uie2.Text;
                        }
                    }

                    // Add row to dictionary
                    if ((fieldKey != "") && (fieldValue != "") && (dict.ContainsKey(fieldKey) == false))
                    {
                        dict.Add(fieldKey, fieldValue);
                    }
                }

                // Add items in dictionary to array
                if (dict.Count > 0)
                {
                    // Create a AuthenticationDataElement array
                    AuthenticationDataElement[] adea = dict.Select(pair => new AuthenticationDataElement()
                    {
                        Key = pair.Key,
                        Value = pair.Value
                    }).ToArray();

                    return adea;
                }
                else // No items in dictionary
                {
                    return null;
                }
            }
            else // No fields to save
            {
                return null;
            }
        }

        public static void AuthenticationDataElementToGrid(Request request)
        {
            // Generate Autherntication Fields
            AuthenticationFields.GenerateFields();

            // Get the AuthenticationGrid from the MainWindow
            Grid AuthenticationGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationGrid;

            // Populate Fields, skiping the first two UIElements
            for (int i = 2; i < AuthenticationGrid.Children.Count; i++)
            {
                // Check if the next UIElement is a Label
                if (AuthenticationGrid.Children.Cast<UIElement>().ElementAt(i).GetType() == typeof(Label))
                {
                    Label label = ((Label)AuthenticationGrid.Children.Cast<UIElement>().ElementAt(i));
                    
                    // See if the Label's Name contains the word "Key"
                    if (label.Name.Contains("Key"))
                    {
                        foreach (AuthenticationDataElement ade in request.AuthenticationFields)
                        {
                            if (label.Content.ToString().Contains(ade.Key))
                            {
                                // Set the Text of the value TextBox
                                ((TextBox)AuthenticationGrid.Children.Cast<UIElement>().ElementAt(i + 1)).Text = ade.Value;
                            }
                        }
                    }
                }
            }

            // Set the AuthenticationGrid of Main window to the AuthenticationGrid variable
            ((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationGrid = AuthenticationGrid;
        }
    }
}