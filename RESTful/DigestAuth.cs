using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;

using System.Security.Cryptography;

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

            // Get the method from the MainWindow
            ComboBox httpMethod = ((MainWindow)System.Windows.Application.Current.MainWindow).HttpMethod;

            // Get the URI from the MainWindow
            TextBox uri = ((MainWindow)System.Windows.Application.Current.MainWindow).URI;

            // Get the Body from the MainWindow
            TextBox body = ((MainWindow)System.Windows.Application.Current.MainWindow).RequestBody;

            // http://en.wikipedia.org/wiki/Digest_access_authentication#Overview

            // Create A1, HA1, A2, and HA2 variables to hold MD5 hashes
            string A1 = null;
            string HA1 = null;
            string A2 = null;
            string HA2 = null;

            // Create H1 based off of algorithm
            if ((algorithmTextBox.Text == "MD5") || (algorithmTextBox.Text == ""))
            {
                // HA1 = MD5(A1) = MD5(username:realm:password)

                // A1 = MD5(username:realm:password)
                MD5 md5A1 = MD5.Create();
                // convert the input string to a byte array and compute its hash
                byte[] data1 = md5A1.ComputeHash(Encoding.UTF8.GetBytes(usernameTextBox.Text + ":" + realmTextBox.Text + ":" + passwordTextBox.Text));
                // create a new stringbuilder to collect the bytes and create a string
                StringBuilder sB1 = new StringBuilder();
                // loop through each byte of hashed data and format each one as a hexadecimal string
                for (int i = 0; i < data1.Length; i++)
                {
                    sB1.Append(data1[i].ToString("x2"));
                }
                // Save the String
                A1 = sB1.ToString();

                // HA1 = MD5(A1)
                MD5 md5HA1 = MD5.Create();
                // convert the input string to a byte array and compute its hash
                byte[] data2 = md5HA1.ComputeHash(Encoding.UTF8.GetBytes(A1));
                // create a new stringbuilder to collect the bytes and create a string
                StringBuilder sB2 = new StringBuilder();
                // loop through each byte of hashed data and format each one as a hexadecimal string
                for (int i = 0; i < data2.Length; i++)
                {
                    sB2.Append(data2[i].ToString("x2"));
                }
                // Save the String
                HA1 = sB1.ToString();
            }
            else if (algorithmTextBox.Text == "MD5-sess")
            {
                // HA1 = MD5(A1) = MD5(MD5(username:realm:password):nonce:cnonce)

                // MD5(username:realm:password)
                MD5 md5 = MD5.Create();
                // convert the input string to a byte array and compute its hash
                byte[] data1 = md5.ComputeHash(Encoding.UTF8.GetBytes(usernameTextBox.Text + ":" + realmTextBox.Text + ":" + passwordTextBox.Text));
                // create a new stringbuilder to collect the bytes and create a string
                StringBuilder sB1 = new StringBuilder();
                // loop through each byte of hashed data and format each one as a hexadecimal string
                for (int i = 0; i < data1.Length; i++)
                {
                    sB1.Append(data1[i].ToString("x2"));
                }
                // Save the String
                string md5String = sB1.ToString();

                // A1 = MD5(MD5(username:realm:password):nonce:cnonce)
                MD5 md5A1 = MD5.Create();
                // convert the input string to a byte array and compute its hash
                byte[] data2 = md5.ComputeHash(Encoding.UTF8.GetBytes(md5String + ":" + nonceTextBox.Text + ":" + clientNonceTextBox.Text));
                StringBuilder sB2 = new StringBuilder();
                // loop through each byte of hashed data and format each one as a hexadecimal string
                for (int i = 0; i < data2.Length; i++)
                {
                    sB1.Append(data2[i].ToString("x2"));
                }
                // Save the String
                A1 = sB1.ToString();

                // HA1 = MD5(A1)
                MD5 md5HA1 = MD5.Create();
                // convert the input string to a byte array and compute its hash
                byte[] data3 = md5HA1.ComputeHash(Encoding.UTF8.GetBytes(A1));
                // create a new stringbuilder to collect the bytes and create a string
                StringBuilder sB3 = new StringBuilder();
                // loop through each byte of hashed data and format each one as a hexadecimal string
                for (int i = 0; i < data3.Length; i++)
                {
                    sB2.Append(data3[i].ToString("x2"));
                }
                // Save the String
                HA1 = sB3.ToString();
            }

            // Create H2 based off of qop
            if ((qopTextBox.Text == "auth") || (qopTextBox.Text == ""))
            {
                // A2 = MD5(method:digestURI)
                MD5 md5A2 = MD5.Create();
                // convert the input string to a byte array and compute its hash
                byte[] data = md5A2.ComputeHash(Encoding.UTF8.GetBytes(httpMethod.SelectedValue.ToString() + ":" + uri.Text));
                // create a new stringbuilder to collect the bytes and create a string
                StringBuilder sB = new StringBuilder();
                // loop through each byte of hashed data and format each one as a hexadecimal string
                for (int i = 0; i < data.Length; i++)
                {
                    sB.Append(data[i].ToString("x2"));
                }
                // Save the String
                A2 = sB.ToString();
            }
            else if (qopTextBox.Text == "auth-int")
            {
                // HA2 = MD5(A2) = MD5(method:digestURI:MD5(entityBody))

                // MD5(entityBody)
                MD5 md5 = MD5.Create();
                // convert the input string to a byte array and compute its hash
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(body.Text));
                // create a new stringbuilder to collect the bytes and create a string
                StringBuilder sB1 = new StringBuilder();
                // loop through each byte of hashed data and format each one as a hexadecimal string
                for (int i = 0; i < data.Length; i++)
                {
                    sB1.Append(data[i].ToString("x2"));
                }
                // Save the String
                string md5String = sB1.ToString();
                
                // A2 = MD5(method:digestURI:MD5(entityBody))
                MD5 md5A2 = MD5.Create();
                // convert the input string to a byte array and compute its hash
                byte[] data2 = md5A2.ComputeHash(Encoding.UTF8.GetBytes(httpMethod.SelectedValue.ToString() + ":" + uri.Text + ":" + md5String));
                // create a new stringbuilder to collect the bytes and create a string
                StringBuilder sB2 = new StringBuilder();
                // loop through each byte of hashed data and format each one as a hexadecimal string
                for (int i = 0; i < data2.Length; i++)
                {
                    sB2.Append(data2[i].ToString("x2"));
                }
                // Save the String
                A2 = sB2.ToString();

                //HA2 = MD5(A2)
                MD5 md5HA2 = MD5.Create();
                // convert the input string to a byte array and compute its hash
                byte[] data3 = md5HA2.ComputeHash(Encoding.UTF8.GetBytes(A2));
                // create a new stringbuilder to collect the bytes and create a string
                StringBuilder sB3 = new StringBuilder();
                // loop through each byte of hashed data and format each one as a hexadecimal string
                for (int i = 0; i < data3.Length; i++)
                {
                    sB3.Append(data3[i].ToString("x2"));
                }
                // Save the String
                HA2 = sB3.ToString();
            }

            string encodedCredentials = null;

            if (HA1 != null)
            {
                encodedCredentials = HA1;
            }
            else if (HA2 != null)
            {
                encodedCredentials = HA2;
            }

            return new KeyValuePair<string, string>("Authentication", "Digest " + encodedCredentials);
        }
    }
}
