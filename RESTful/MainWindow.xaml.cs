using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net.Http;
using System.Net.Http.Formatting;

using System.Reflection;

namespace RESTful
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HTTPProtocol(object sender, RoutedEventArgs e)
        {
            // Get the ComboBox reference
            var comboBox = sender as ComboBox;

            // Assign the list from HTTPProtocols.cs to the ItemsSource
            comboBox.ItemsSource = HTTPProtocols.HTTPProtocolsList;
        }

        private void HTTPVerb(object sender, RoutedEventArgs e)
        {
            // Get the ComboBox reference
            var comboBox = sender as ComboBox;

            // Assign the list from HTTPVerbs.cs to the ItemsSource
            comboBox.ItemsSource = HTTPVerbs.HTTPVerbsList;
        }

        private void AssemblyBrowse_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".dll";
            dlg.Filter = "Assemblies| *.dll";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Write the result to the text block x:Name="AssemblyFilePath"
                AssemblyFilePath.Text = dlg.FileName;
            }

            // Poputlate Types
            PopulateTypes(sender, e);
        }

        private void AttachmentBrowse_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Write the result to the text block x:Name="AttachmentFilePath"
                AttachmentFilePath.Text = dlg.FileName;
            }
        }

        private void PopulateTypes(object sender, RoutedEventArgs e)
        {
            // Ensure user defined inputs have value
            if ((AssemblyFilePath.Text != null) && (AssemblyFilePath.Text != "") && (AssemblyFilePath.Text.EndsWith(".dll")))
            {
                // Load the DLL from the ImportFile value
                Assembly importedDLL = Assembly.LoadFrom(AssemblyFilePath.Text);

                // Build a List of strings, containing the options
                List<string> types = new List<string>();
                foreach (Type t in importedDLL.GetTypes())
                {
                    types.Add(t.ToString());
                }

                // Assign the ItemsSource to the List
                Types.ItemsSource = types;
            }
        }

        private void PopulateFormats(object sender, RoutedEventArgs e)
        {
            // Get the ComboBox reference
            var comboBox = sender as ComboBox;

            // Assign the list from Formats.cs to the ItemsSource
            comboBox.ItemsSource = Formats.FormatList;          
        }

        private void PopulateRequestBody_Click(object sender, RoutedEventArgs e)
        {
            // Save selection settings
            SaveImportInputs();

            // Ensure user defined inputs have value
            if ((AssemblyFilePath.Text != null) && (Types.SelectedValue != null) && (Types.SelectedValue.ToString() != ""))
            {
                if (Format.SelectedValue.ToString() == "JSON")
                {
                    // Build a JSON template string and write the result to the text block x:Name="Body"
                    RequestBody.Text = Templates.JSONTemplate();
                }
                else if (Format.SelectedValue.ToString() == "XML")
                {
                    // Build ax XML template string and write the result to the text block x:Name="Body"
                    RequestBody.Text = Templates.XMLTemplate();
                }
            }
        }

        void submit_Button(object sender, RoutedEventArgs e)
        {
            // Save current request
            SaveRequestInputs();

            // Send the request
            HttpResponseMessage result = SendRequest.Send();
            
            // Check result
            if (result != null)
            {
                // Write the result to the text blocks x:Name="ResponseHeader" and x:Name="ResponseBody"
                ResponseHeader.Text = result.Headers.ToString();
                ResponseBody.Text = result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                // Print error message
                ResponseBody.Text = "There was an error with your request. Please check your input and try again.";
            }
        }

        void SaveRequestInputs()
        {
            RESTful.Properties.Settings.Default.Protocol = Protocol.SelectedValue.ToString();
            RESTful.Properties.Settings.Default.Method = Method.SelectedValue.ToString();
            RESTful.Properties.Settings.Default.BaseAddress = BaseAddress.Text;
            RESTful.Properties.Settings.Default.Resource = Resource.Text;
            RESTful.Properties.Settings.Default.AttachmentFile = AttachmentFilePath.Text;
            RESTful.Properties.Settings.Default.RequestHeader = RequestHeader.Text;
            RESTful.Properties.Settings.Default.RequestBody = RequestBody.Text;
            RESTful.Properties.Settings.Default.Save();
        }

        void SaveImportInputs()
        {
            RESTful.Properties.Settings.Default.ImportAssembly = AssemblyFilePath.Text;
            RESTful.Properties.Settings.Default.Type = Types.SelectedValue.ToString()?? "";
            RESTful.Properties.Settings.Default.Format = Format.SelectedValue.ToString() ?? "";
            RESTful.Properties.Settings.Default.Save();
        }

    }
}
