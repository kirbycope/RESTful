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

        private void NewRequest_Click(object sender, MouseButtonEventArgs e)
        {
            // Clear Authentication
            AuthenticationMethod.SelectedIndex = -1;
            ClearAuthenticationFields();
            // Clear Protocol
            ProtocolVersion.SelectedIndex = -1;
            // Clear Method
            HttpMethod.SelectedIndex = -1;
            // Clear URI
            URI.Text = "";
            // Clear Parameters
            RemoveAllParameters_Click(sender, e);
            // Clear Headers
            RemoveAllHeaders_Click(sender, e);
            // Clear Attachment
            AttachmentPath.Text = "";
            // Clear Body
            RequestBody.Text = "";
            // Clear Errors/Warnings
            Warnings.Opacity = 0;
            ValidationWarnings.Text = "";
            Errors.Opacity = 0;
            ValidationErrors.Text = "";
            // Clear Response
            ResponseHeader.Text = "";
            ResponseBody.Text = "";
        }

        private void SaveRequest_Click(object sender, MouseButtonEventArgs e)
        {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".rest";
            dlg.Filter = "RESTful binary (*.rest)|*.rest";
            dlg.AddExtension = true;

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                SaveRequest.ToBinary(dlg.FileName);
            }
        }

        private void OpenRequest_Click(object sender, MouseButtonEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".rest";
            dlg.Filter = "RESTful binary (*.rest)|*.rest";

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Clear any existing fields
                NewRequest_Click(sender, e);

                // Load vaules into fields
                LoadRequest.FromBinary(dlg.FileName);
            }
        }        

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Populate_AuthenticationMethods(object sender, RoutedEventArgs e)
        {
            // Get the ComboBox reference
            var comboBox = sender as ComboBox;

            // Assign the list from AuthenticationMethods.cs to the ItemsSource
            comboBox.ItemsSource = AuthenticationMethods.AuthenticationMethodsList;
        }

        private void AuthenticationMethod_DropDownClosed(object sender, EventArgs e)
        {
            // Clear existing fields
            ClearAuthenticationFields();

            // Generate Autherntication Fields
            AuthenticationFields.GenerateFields();
        }

        private void ClearAuthenticationFields()
        {
            if (AuthenticationGrid.RowDefinitions.Count > 1)
            {
                // Delete all the children except the ComboBox row
                AuthenticationGrid.Children.RemoveRange(2, (AuthenticationGrid.Children.Count - 2));

                // Delete the now unused RowDefinitions
                AuthenticationGrid.RowDefinitions.RemoveRange(1, (AuthenticationGrid.RowDefinitions.Count - 1));
            }
        }

        private void Populate_ProtocolVersions(object sender, RoutedEventArgs e)
        {
            // Get the ComboBox reference
            var comboBox = sender as ComboBox;

            // Assign the list from HTTPProtocols.cs to the ItemsSource
            comboBox.ItemsSource = HTTPProtocols.HTTPProtocolsList;
        }

        private void Populate_HttpMethods(object sender, RoutedEventArgs e)
        {
            // Get the ComboBox reference
            var comboBox = sender as ComboBox;

            // Assign the list from HTTPMethods.cs to the ItemsSource
            comboBox.ItemsSource = HTTPMethods.HTTPMethodsList;
        }

        private void RemoveAllParameters_Click(object sender, MouseButtonEventArgs e)
        {
            if (ParametersGrid.Children.Count > 4)
            {
                // Create a list to hold each element to delete
                List<UIElement> elementsToDelete = new List<UIElement>();

                // Add elements to list
                foreach (UIElement uie in ParametersGrid.Children)
                {
                    if (((string)uie.GetValue(NameProperty) != "RemoveAllParametersLabel") && ((string)uie.GetValue(NameProperty) != "ParameterFieldLabel") && ((string)uie.GetValue(NameProperty) != "ParameterValueLabel") && ((string)uie.GetValue(NameProperty) != "AddParameter"))
                    {
                        elementsToDelete.Add(uie);
                    }
                }

                // Remove elements in list from Parameters Grid
                if (elementsToDelete != null)
                {
                    foreach (UIElement uie in elementsToDelete)
                    {
                        ParametersGrid.Children.Remove(uie);
                    }
                }
            }
        }

        public void RemoveParameter_Click(object sender, MouseButtonEventArgs e)
        {
            // Get the Label reference
            Label removeParameterLabel = sender as Label;

            // Get the Label's row
            int labelRow = (int)removeParameterLabel.GetValue(Grid.RowProperty);

            // Get the row's children
            UIElement header = ParametersGrid.Children.Cast<UIElement>().Where(i => Grid.GetRow(i) == labelRow && Grid.GetColumn(i) == 0).First();
            UIElement value = ParametersGrid.Children.Cast<UIElement>().Where(i => Grid.GetRow(i) == labelRow && Grid.GetColumn(i) == 1).First();
            UIElement label = ParametersGrid.Children.Cast<UIElement>().Where(i => Grid.GetRow(i) == labelRow && Grid.GetColumn(i) == 2).First();

            // Remove the row's children
            ParametersGrid.Children.Remove(header);
            ParametersGrid.Children.Remove(value);
            ParametersGrid.Children.Remove(label);
        }

        private void AddParameter_Click(object sender, MouseButtonEventArgs e)
        {
            ParameterFields.AddField();
        }

        private void RemoveAllHeaders_Click(object sender, MouseButtonEventArgs e)
        {
            if (HeadersGrid.Children.Count > 4)
            {
                // Create a list to hold each element to delete
                List<UIElement> elementsToDelete = new List<UIElement>();

                // Add elements to list
                foreach (UIElement uie in HeadersGrid.Children)
                {
                    if (((string)uie.GetValue(NameProperty) != "RemoveAllHeadersLabel") && ((string)uie.GetValue(NameProperty) != "HeaderFieldLabel") && ((string)uie.GetValue(NameProperty) != "HeaderValueLabel") && ((string)uie.GetValue(NameProperty) != "AddHeader"))
                    {
                        elementsToDelete.Add(uie);
                    }
                }

                // Remove elements in list from Parameters Grid
                if (elementsToDelete != null)
                {
                    foreach (UIElement uie in elementsToDelete)
                    {
                        HeadersGrid.Children.Remove(uie);
                    }
                }
            }
        }

        public void RemoveHeader_Click(object sender, MouseButtonEventArgs e)
        {
            // Get the Label reference
            Label removeHeaderLabel = sender as Label;

            // Get the Label's row
            int labelRow = (int)removeHeaderLabel.GetValue(Grid.RowProperty);

            // Get the row's children
            UIElement header = HeadersGrid.Children.Cast<UIElement>().Where(i => Grid.GetRow(i) == labelRow && Grid.GetColumn(i) == 0).First();
            UIElement value = HeadersGrid.Children.Cast<UIElement>().Where(i => Grid.GetRow(i) == labelRow && Grid.GetColumn(i) == 1).First();
            UIElement label = HeadersGrid.Children.Cast<UIElement>().Where(i => Grid.GetRow(i) == labelRow && Grid.GetColumn(i) == 2).First();

            // Remove the row's children
            HeadersGrid.Children.Remove(header);
            HeadersGrid.Children.Remove(value);
            HeadersGrid.Children.Remove(label);
        }

        private void AddHeader_Click(object sender, MouseButtonEventArgs e)
        {
            HeaderFields.AddField();
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
                // Write the result to the text block x:Name="AssemblyFilePath"
                AttachmentPath.Text = dlg.FileName;
            }
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
                AssemblyPath.Text = dlg.FileName;
            }

            // Ensure user defined inputs have value
            if ((AssemblyPath.Text != null) && (AssemblyPath.Text != "") && (AssemblyPath.Text.EndsWith(".dll")))
            {
                // Load the imported assembly
                Assembly importedAssembly = ReflectedAssembly.Assembly;

                // Build a List of strings, containing the options
                List<string> types = new List<string>();
                foreach (Type t in importedAssembly.GetTypes())
                {
                    types.Add(t.ToString());
                }

                // Assign the ItemsSource to the list for x:Name="Types"
                Types.ItemsSource = types;
            }
        }

        private void PopulateFormats(object sender, RoutedEventArgs e)
        {
            // Get the ComboBox reference
            var comboBox = sender as ComboBox;

            // Assign the list from Formats.cs to the ItemsSource
            comboBox.ItemsSource = EncodingFormats.FormatList;
        }

        private void GenerateTemplate_Click(object sender, RoutedEventArgs e)
        {

            // Ensure user defined inputs have value
            if ((AssemblyPath.Text != null) && (AssemblyPath.Text != "") && (Types.SelectedValue != null) && (Types.SelectedValue.ToString() != "") && (Formats.Text != null) && (Formats.Text != ""))
            {
                if (Formats.SelectedValue.ToString() == "JSON")
                {
                    // Build a JSON template string and write the result to the text block x:Name="Body"
                    RequestBody.Text = Templates.JSONTemplate();
                }
                else if (Formats.SelectedValue.ToString() == "XML")
                {
                    // Build ax XML template string and write the result to the text block x:Name="Body"
                    RequestBody.Text = Templates.XMLTemplate();
                }
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // Validate user inputs
            ValidateFields.VerifyInputs();

            // Add Parameters to URI
            UriParameters.AddToURI();

            // Send the request
            HttpResponseMessage result = SendRequest.Send();

            // Check result
            if (result != null)
            {
                // Write the result to the text blocks x:Name="ResponseHeader" and x:Name="ResponseBody"
                ResponseHeader.Text = result.Headers.ToString();
                ResponseBody.Text = result.Content.ReadAsStringAsync().Result;

                // Expand Response Expander
                ResponseExpander.IsExpanded = true;
            }
            else
            {
                // Print error message
                ValidationErrors.Text = ValidationErrors.Text + System.Environment.NewLine + "There was an error with your request. Please check your input and try again.";
            }
        }

    }
}