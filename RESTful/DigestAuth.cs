using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;

namespace RESTful
{
    class DigestAuth
    {
        public static void GenereateFields(Grid authGrid)
        {
            // Build a list of Digest Auth Fields
            List<string> fields = new List<string>();
            fields.Add("Username");
            fields.Add("Realm");
            fields.Add("Password");
            fields.Add("Nonce");
            fields.Add("Qop");
            fields.Add("Nonce Count");
            fields.Add("Client Nonce");
            fields.Add("Opaque");

            for (int i = 0; i < fields.Count; i++ )
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
    }
}
