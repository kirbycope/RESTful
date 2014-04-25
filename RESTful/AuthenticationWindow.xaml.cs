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

namespace RESTful
{
    /// <summary>
    /// Interaction logic for AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window
    {
        public AuthenticationWindow()
        {
            InitializeComponent();
            PopulateFields();
        }

        private void PopulateFields()
        {
            // Get the Authentication Method selected by the user
            string authenticationMethod = RESTful.Properties.Settings.Default.AuthenticationMethod;

            if (authenticationMethod == "Basic")
            {
                BasicAuth.GenereateFields(authGrid);
            }
        }

        private void BuildAuthentication_Click(object sender, RoutedEventArgs e)
        {
            // Get the Authentication Method selected by the user
            string authenticationMethod = RESTful.Properties.Settings.Default.AuthenticationMethod;

            if (authenticationMethod == "Basic")
            {
                // Send Grid to be parsed and saved to Settings
                BasicAuth.BuildBasicAuth(authGrid);

                // Update Headers in main window
                var main = App.Current.MainWindow as MainWindow;
                main.PopulateHeaders(sender, e);               
            }

            // Close the AuthenticationWindow
            this.Close();
        }
    }
}