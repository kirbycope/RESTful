﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RESTful
{
    class LoadRequest
    {
        public static void FromBinary(string FilePath)
        {
            // Create a new instance of Request data type
            Request request = new Request();

            // Read in binary file and save to request
            using (Stream stream = File.Open(FilePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                request = (Request)binaryFormatter.Deserialize(stream);
            }

            // Load values into UI

                // Authentication
                if (request.Authentication != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationMethod.SelectedValue = request.Authentication;
                }

                // Authentication Fields
                //request.AuthenticationFields

                // Protocol
                if (request.Protocol != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).ProtocolVersion.SelectedValue = request.Protocol;
                }

                // Method
                if (request.Method != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).HttpMethod.SelectedValue = request.Method;
                }

                // URI
                if (request.URI != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).URI.Text = request.URI;
                }

                // Parameters
                //request.Parameters

                // Headers
                //request.Headers

                // Attachment
                if (request.Attachment != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).AttachmentPath.Text = request.Attachment;
                }

                // Assembly
                if (request.Assembly != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).AssemblyPath.Text = request.Assembly;

                    // Load the imported assembly
                    Assembly importedAssembly = ReflectedAssembly.Assembly;

                    // Build a List of strings, containing the options
                    List<string> types = new List<string>();
                    foreach (Type t in importedAssembly.GetTypes())
                    {
                        types.Add(t.ToString());
                    }

                    // Assign the ItemsSource to the list for x:Name="Types"
                    ((MainWindow)System.Windows.Application.Current.MainWindow).Types.ItemsSource = types;
                }

                // Type
                if (request.Type != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue = request.Type;
                }

                // Format
                if (request.Format != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).Formats.SelectedValue = request.Format;
                }

                // Body
                if (request.Body != null)
                {
                    ((MainWindow)System.Windows.Application.Current.MainWindow).RequestBody.Text = request.Body;
                }
        }
    }
}