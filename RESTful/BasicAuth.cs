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
            // Build a list of Digest Auth Fields
            List<string> fields = new List<string>();
            fields.Add("Username");
            fields.Add("Password");

            for (int i = 0; i < fields.Count; i++)
            {
                // Add a row to the AuthGrid
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = GridLength.Auto;
                authGrid.RowDefinitions.Add(rowDefinition);

                // Add a Label
                Label label = new Label();
                label.SetValue(Grid.RowProperty, i + 1);
                label.SetValue(Grid.ColumnProperty, 0);
                label.Content = fields[i] + ":";
                authGrid.Children.Add(label);

                // Add a textbox
                TextBox textBox = new TextBox();
                textBox.SetValue(Grid.RowProperty, i + 1);
                textBox.SetValue(Grid.ColumnProperty, 1);
                authGrid.Children.Add(textBox);
            }
        }
        
        public static void BuildBasicAuth(Grid authGrid)
        {
            // Get the UIElement at Row=1 Column=0 (Username)
            UIElement usernameUIElement = authGrid.Children
                            .Cast<UIElement>()
                            .First(c => Grid.GetRow(c) == 1 && Grid.GetColumn(c) == 1);

            // Cast the UIElement as a TextBox
            TextBox usernameTextBox = (TextBox)usernameUIElement;

            // Get the UIElement at Row=1 Column=0 (Password)
            UIElement passwordUIElement = authGrid.Children
                            .Cast<UIElement>()
                            .First(c => Grid.GetRow(c) == 2 && Grid.GetColumn(c) == 1);

            // Cast the UIElement as a TextBox
            TextBox passwordTextBox = (TextBox)passwordUIElement;

            // Encode the username:password
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", usernameTextBox.Text, passwordTextBox.Text));
            string headerValue = System.Convert.ToBase64String(plainTextBytes);

            // Get the existing HttpHeader(s)
            string existingHeader = RESTful.Properties.Settings.Default.RequestHeader;

            // TODO: Remove existing authentication from the string

            // Append the new values to the existing headers
            StringBuilder sb = new StringBuilder(existingHeader);
            sb.AppendLine("Authentication:" + "Basic " + headerValue);
            string newHeader = sb.ToString();

            // Save new headers
            RESTful.Properties.Settings.Default.RequestHeader = newHeader;
            RESTful.Properties.Settings.Default.Save();
        }
    }
}