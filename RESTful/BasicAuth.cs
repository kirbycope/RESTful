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
        public static void GenereateFields()
        {
            // Get the Grid from the MainWindow
            Grid AuthenticationGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationGrid;

            // Build a list of Digest Auth Fields
            List<string> fields = new List<string>();
            fields.Add("Username");
            fields.Add("Password");

            for (int i = 0; i < fields.Count; i++)
            {
                // Add a row to the AuthGrid
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = GridLength.Auto;
                AuthenticationGrid.RowDefinitions.Add(rowDefinition);

                // Add a Label
                Label label = new Label();
                label.SetValue(Grid.RowProperty, i + 1);
                label.SetValue(Grid.ColumnProperty, 0);
                label.Name = "AuthenticationKey" + i;
                label.Content = fields[i] + ":";
                AuthenticationGrid.Children.Add(label);

                // Add a textbox
                TextBox textBox = new TextBox();
                textBox.SetValue(Grid.RowProperty, i + 1);
                textBox.SetValue(Grid.ColumnProperty, 1);
                textBox.Name = "AuthenticationValue" + i;
                AuthenticationGrid.Children.Add(textBox);
            }
        }

        public static KeyValuePair<string, string> GetInputs()
        {
            // Get the Grid from the MainWindow
            Grid AuthenticationGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationGrid;

            // Get the username TextBox
            TextBox usernameTextBox = (TextBox)AuthenticationGrid.Children.Cast<UIElement>().First(c => Grid.GetRow(c) == 1 && Grid.GetColumn(c) == 1);

            // Get the password TextBox
            TextBox passwordTextBox = (TextBox)AuthenticationGrid.Children.Cast<UIElement>().First(c => Grid.GetRow(c) == 2 && Grid.GetColumn(c) == 1);

            // http://en.wikipedia.org/wiki/Basic_access_authentication#Client_side

            // Encode the username:password
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", usernameTextBox.Text, passwordTextBox.Text));
            string encodedCredentials = System.Convert.ToBase64String(plainTextBytes);

            return new KeyValuePair<string, string>("Authentication", "Basic " + encodedCredentials);
        }
    }
}