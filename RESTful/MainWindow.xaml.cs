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

        }

        private void OpenRequest_Click(object sender, MouseButtonEventArgs e)
        {

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
            string authenticationMethod = null;

            if (AuthenticationMethod.SelectedValue != null)
            {
                // Get the selected authentication method
                authenticationMethod = AuthenticationMethod.SelectedValue.ToString();
            }
            
            if ((authenticationMethod != null) && (authenticationMethod != ""))
            {
                if (authenticationMethod == "None")
                {
                    if (AuthenticationGrid.RowDefinitions.Count > 1)
                    {
                        // Clear existing fields
                        ClearAuthenticationFields();
                    }
                }
                else if (authenticationMethod == "Basic")
                {
                    // Clear existing fields
                    ClearAuthenticationFields();
                    
                    // Populate fields
                    BasicAuth.GenereateFields();
                }
                else if (authenticationMethod == "Digest")
                {
                    // Clear existing fields
                    ClearAuthenticationFields();

                    // Populate fields
                    DigestAuth.GenereateFields();
                }
                else if (authenticationMethod == "OAuth1")
                {
                    // Clear existing fields
                    ClearAuthenticationFields();

                    // Populate fields
                    OAuth1.GenereateFields();
                }
                else if (authenticationMethod == "OAuth2")
                {
                    // Clear existing fields
                    ClearAuthenticationFields();

                    // Populate fields
                    OAuth2.GenereateFields();
                }
            }
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

        private void RemoveParameter_Click(object sender, MouseButtonEventArgs e)
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
            // Get Current Header count
            int headerCount = ParametersGrid.RowDefinitions.Count();

            // Add a header/key textbox
            TextBox header = new TextBox();
            header.SetValue(Grid.RowProperty, headerCount - 1);
            header.SetValue(Grid.ColumnProperty, 0);
            header.Name = String.Format("requestParameterKey{0}", headerCount);
            ParametersGrid.Children.Add(header);

            // Add a value textbox
            TextBox value = new TextBox();
            value.SetValue(Grid.RowProperty, headerCount - 1);
            value.SetValue(Grid.ColumnProperty, 1);
            value.Name = String.Format("requestParameterValue{0}", headerCount);
            ParametersGrid.Children.Add(value);

            // Add the remove button
            Label removeParameterButton = new Label();
            removeParameterButton.SetValue(Grid.RowProperty, headerCount - 1);
            removeParameterButton.SetValue(Grid.ColumnProperty, 2);
            removeParameterButton.MouseDown += RemoveParameter_Click;
            removeParameterButton.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "/Resources/#GLYPHICONS Halflings");
            removeParameterButton.ToolTip = "Remove Parameter";
            removeParameterButton.Foreground = Brushes.DarkRed;
            removeParameterButton.FontSize = 16;
            removeParameterButton.Content = "\ue083";  //"&#57475;"
            removeParameterButton.HorizontalAlignment = HorizontalAlignment.Center;
            ParametersGrid.Children.Add(removeParameterButton);

            // Add a row to the ParametersGrid
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;
            ParametersGrid.RowDefinitions.Add(rowDefinition);

            // Move the Add Header button down a row
            AddParameter.SetValue(Grid.RowProperty, (headerCount + 1));
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

        public void RemoveHeader_Click(object sender, MouseButtonEventArgs e) // Needs work on removing children
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
            // Get Current Header count
            int headerCount = HeadersGrid.RowDefinitions.Count();

            // Add a header/key textbox
            TextBox header = new TextBox();
            header.SetValue(Grid.RowProperty, headerCount - 1);
            header.SetValue(Grid.ColumnProperty, 0);
            header.Name = String.Format("requestHeaderKey{0}", headerCount);
            HeadersGrid.Children.Add(header);

            // Add a value textbox
            TextBox value = new TextBox();
            value.SetValue(Grid.RowProperty, headerCount -1);
            value.SetValue(Grid.ColumnProperty, 1);
            value.Name = String.Format("requestHeaderValue{0}", headerCount);
            HeadersGrid.Children.Add(value);

            // Add the remove button
            Label removeHeaderButton = new Label();
            removeHeaderButton.SetValue(Grid.RowProperty, headerCount - 1);
            removeHeaderButton.SetValue(Grid.ColumnProperty, 2);
            removeHeaderButton.MouseDown += RemoveHeader_Click;
            removeHeaderButton.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "/Resources/#GLYPHICONS Halflings");
            removeHeaderButton.ToolTip = "Remove Header";
            removeHeaderButton.Foreground = Brushes.DarkRed;
            removeHeaderButton.FontSize = 16;
            removeHeaderButton.Content = "\ue083";  //"&#57475;"
            removeHeaderButton.HorizontalAlignment = HorizontalAlignment.Center;
            HeadersGrid.Children.Add(removeHeaderButton);

            // Add a row to the HeadersGrid
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = GridLength.Auto;
            HeadersGrid.RowDefinitions.Add(rowDefinition);

            // Move the Add Header button down a row
            AddHeader.SetValue(Grid.RowProperty, (headerCount + 1));
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