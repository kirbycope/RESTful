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
using System.Windows.Shapes;

namespace RESTful
{
    /// <summary>
    /// Interaction logic for ParameterWindow.xaml
    /// </summary>
    public partial class ParameterWindow : Window
    {
        public ParameterWindow()
        {
            InitializeComponent();
        }

        private void BuildParameters_Click(object sender, RoutedEventArgs e)
        {
            // Create a stringbuilder
            StringBuilder sb = new StringBuilder(RESTful.Properties.Settings.Default.URI);
            
            // Check if there is already a querystring parameter
            if (sb.ToString().Contains('?'))
            {
                sb.Append('&');
            }
            else
            {
                sb.Append('?');
            }

            // Add new parameter
            sb.Append(Field.Text + "=" + Value.Text);

            // Update the URI in the Settings
            RESTful.Properties.Settings.Default.URI = sb.ToString();
            RESTful.Properties.Settings.Default.Save();

            // Update URI in the MainWindow
            var main = App.Current.MainWindow as MainWindow;
            main.PopulateURI(sender, e);  

            // Close the ParameterWindow
            this.Close();
        }
    }
}
