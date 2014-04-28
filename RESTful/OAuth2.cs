using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;

namespace RESTful
{
    class OAuth2
    {
        public static void GenereateFields(Grid AuthenticationGrid)
        {
            // Build a list of Digest Auth Fields
            List<string> fields = new List<string>();
            fields.Add("Callback URL");
            fields.Add("Authorization URL");
            fields.Add("Access Token URL");
            fields.Add("Client ID");
            fields.Add("Client Secret");
            fields.Add("Scope (optional)");

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
                label.Content = fields[i] + ":";
                AuthenticationGrid.Children.Add(label);

                // Add a textbox
                TextBox textBox = new TextBox();
                textBox.SetValue(Grid.RowProperty, i + 1);
                textBox.SetValue(Grid.ColumnProperty, 1);
                AuthenticationGrid.Children.Add(textBox);
            }
        }
    }
}