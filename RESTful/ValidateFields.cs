using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTful
{
    class ValidateFields
    {
        public static void VerifyInputs()
        {
            // Create new instances of StringBuilder
            StringBuilder warnings = new StringBuilder();
            StringBuilder errors = new StringBuilder();

            // Authentication Method: If null, then set to "None"
            if (((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationMethod.SelectedValue == null)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationMethod.SelectedValue = "None";
                warnings.AppendLine("Authentication method set to 'None'");
            }

            // HTTP Protocol Version: If null, then set to "1.1"
            if (((MainWindow)System.Windows.Application.Current.MainWindow).ProtocolVersion.SelectedValue == null)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).ProtocolVersion.SelectedValue = "1.1";
                warnings.AppendLine("Protocol version set to '1.1'");
            }

            // HTTP Method (Verb): If null, then ...
            if (((MainWindow)System.Windows.Application.Current.MainWindow).HttpMethod.SelectedValue == null)
            {
                if ((((MainWindow)System.Windows.Application.Current.MainWindow).AttachmentPath.Text == null) || (((MainWindow)System.Windows.Application.Current.MainWindow).AttachmentPath.Text == ""))
                {
                    // ... set to GET
                    ((MainWindow)System.Windows.Application.Current.MainWindow).HttpMethod.SelectedValue = "GET";
                    warnings.AppendLine("HTTP Method set to 'GET'");
                }
                else
                {
                    // ... set to POST
                    ((MainWindow)System.Windows.Application.Current.MainWindow).HttpMethod.SelectedValue = "POST";
                    warnings.AppendLine("HTTP Method set to 'POST'");
                }
                
            }

            // Request URI: If invalid return error message
            if ((Uri.IsWellFormedUriString((((MainWindow)System.Windows.Application.Current.MainWindow).URI.Text), UriKind.RelativeOrAbsolute)) != true) // This returns true if blank :(
            {
                errors.AppendLine("Request URI is invalid");
            }

            // File Attachement

            // Request Headers

            // Request Body

            // Imported Assembly Type

            // Encoding Format

            // Print to UI
            ((MainWindow)System.Windows.Application.Current.MainWindow).Warnings.Opacity = 100;
            ((MainWindow)System.Windows.Application.Current.MainWindow).ValidationWarnings.Text = warnings.ToString();
            ((MainWindow)System.Windows.Application.Current.MainWindow).Errors.Opacity = 100;
            ((MainWindow)System.Windows.Application.Current.MainWindow).ValidationErrors.Text = errors.ToString();
        }
    }
}
