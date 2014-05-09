using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;

namespace RESTful
{
    class OAuth1
    {
        public static void GenereateFields()
        {
            // Get the Grid from the MainWindow
            Grid AuthenticationGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationGrid;

            // Build a list of Digest Auth Fields
            List<string> fields = new List<string>();
            fields.Add("Consumer Key");
            fields.Add("Consumer Secret");
            fields.Add("Token");
            fields.Add("Token Secret");
            fields.Add("Signature Method");
            fields.Add("Timestamp");
            fields.Add("Nonce");
            fields.Add("Version");
            fields.Add("Realm (optional)");

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
    }
}