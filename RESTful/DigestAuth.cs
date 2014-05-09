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
        public static void GenereateFields()
        {
            // Get the Grid from the MainWindow
            Grid AuthenticationGrid = ((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationGrid;

            // Build a list of Digest Auth Fields
            List<string> fields = new List<string>();
            fields.Add("Username");
            fields.Add("Realm");
            fields.Add("Password");
            fields.Add("Nonce");
            fields.Add("Algorithm");
            fields.Add("Qop");
            fields.Add("Nonce Count");
            fields.Add("Client Nonce");
            fields.Add("Opaque");

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

            // Get the realm TextBox
            TextBox realmTextBox = (TextBox)AuthenticationGrid.Children.Cast<UIElement>().First(c => Grid.GetRow(c) == 2 && Grid.GetColumn(c) == 1);
            
            // Get the password TextBox
            TextBox passwordTextBox = (TextBox)AuthenticationGrid.Children.Cast<UIElement>().First(c => Grid.GetRow(c) == 3 && Grid.GetColumn(c) == 1);

            // Get the nonce TextBox
            TextBox nonceTextBox = (TextBox)AuthenticationGrid.Children.Cast<UIElement>().First(c => Grid.GetRow(c) == 4 && Grid.GetColumn(c) == 1);

            // Get the algorithm TextBox
            TextBox algorithmTextBox = (TextBox)AuthenticationGrid.Children.Cast<UIElement>().First(c => Grid.GetRow(c) == 5 && Grid.GetColumn(c) == 1);

            // Get the qop TextBox
            TextBox qopTextBox = (TextBox)AuthenticationGrid.Children.Cast<UIElement>().First(c => Grid.GetRow(c) == 6 && Grid.GetColumn(c) == 1);

            // Get the nonce count TextBox
            TextBox nonceCountTextBox = (TextBox)AuthenticationGrid.Children.Cast<UIElement>().First(c => Grid.GetRow(c) == 7 && Grid.GetColumn(c) == 1);

            // Get the client nonce TextBox
            TextBox clientNonceTextBox = (TextBox)AuthenticationGrid.Children.Cast<UIElement>().First(c => Grid.GetRow(c) == 8 && Grid.GetColumn(c) == 1);

            // Get the opaque TextBox
            TextBox opaqueTextBox = (TextBox)AuthenticationGrid.Children.Cast<UIElement>().First(c => Grid.GetRow(c) == 9 && Grid.GetColumn(c) == 1);

            // http://en.wikipedia.org/wiki/Digest_access_authentication#Overview

            // Create H1 based off of algorithm
            if ((algorithmTextBox.Text == "MD5") || (algorithmTextBox.Text == ""))
            {
                // HA1 = MD5(A1) = MD5(username:realm:password)
            }
            else if (algorithmTextBox.Text == "MD5-sess")
            {
                // HA1 = MD5(A1) = MD5(MD5(username:realm:password):nonce:cnonce)
            }

            // Create H2 based off of qop
            if ((qopTextBox.Text == "auth") || (qopTextBox.Text == ""))
            {
                // HA2 = MD5(A2) = MD5(method:digestURI)
            }
            else if (qopTextBox.Text == "auth-int")
            {
                // HA2 = MD5(A2) = MD5(method:digestURI:MD5(entityBody))
            }

            string encodedCredentials = "";

            return new KeyValuePair<string, string>("Authentication", "Digest " + encodedCredentials);
        }
    }
}
