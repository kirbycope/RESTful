using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;

namespace RESTful
{
    class BasicAuth
    {
        public static void GenereateFields(Grid authGrid)
        {
            // Add a row to the AuthGrid
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;
            authGrid.RowDefinitions.Add(rowDefinition);

            // Add a Username textbox
            TextBox username = new TextBox();
            username.SetValue(Grid.RowProperty, 1);
            username.SetValue(Grid.ColumnProperty, 0);
            username.Name = "Username";
            username.Text = "username";
            authGrid.Children.Add(username);

            // Add a Password textbox
            TextBox password = new TextBox();
            password.SetValue(Grid.RowProperty, 1);
            password.SetValue(Grid.ColumnProperty, 1);
            password.Name = "Password";
            password.Text = "password";
            authGrid.Children.Add(password);
        }
        
        public static void BuildBasicAuth(Grid authGrid)
        {
            // Get the UIElement at Row=1 Column=0 (Username)
            UIElement usernameUIElement = authGrid.Children
                            .Cast<UIElement>()
                            .First(c => Grid.GetRow(c) == 1 && Grid.GetColumn(c) == 0);

            // Cast the UIElement as a TextBox
            TextBox usernameTextBox = (TextBox)usernameUIElement;

            // Get the UIElement at Row=1 Column=0 (Password)
            UIElement passwordUIElement = authGrid.Children
                            .Cast<UIElement>()
                            .First(c => Grid.GetRow(c) == 1 && Grid.GetColumn(c) == 1);

            // Cast the UIElement as a TextBox
            TextBox passwordTextBox = (TextBox)passwordUIElement;

            // Encode the username:password
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", usernameTextBox.Text, passwordTextBox.Text));
            string headerValue = System.Convert.ToBase64String(plainTextBytes);

            // Store the Authentication values to a dictionary
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Authentication", headerValue);

            // Get the existing HttpHeader(s)
            string existingHeader = RESTful.Properties.Settings.Default.RequestHeader;

            // Append the new values to the existing headers
            StringBuilder sb = new StringBuilder(existingHeader);
            sb.AppendLine("Authentication:" + headerValue);
            string newHeader = sb.ToString();

            // Save new headers
            RESTful.Properties.Settings.Default.RequestHeader = newHeader;
            RESTful.Properties.Settings.Default.Save();
        }
    }
}