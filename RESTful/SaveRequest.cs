using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTful
{
    class SaveRequest
    {
        public static void ToBinary(string FilePath)
        {
            // Save inputs to Request data type
            Request request = new Request();

                // Authentication
                if (((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationMethod.SelectedValue != null)
                {
                    request.Authentication = ((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationMethod.SelectedValue.ToString();
                }
                
                // Authentication Fields
                request.AuthenticationFields = AuthenticationFields.GridToAuthenticationDataElement();
                
                // Protocol
                if (((MainWindow)System.Windows.Application.Current.MainWindow).ProtocolVersion.SelectedValue != null)
                {
                    request.Protocol = ((MainWindow)System.Windows.Application.Current.MainWindow).ProtocolVersion.SelectedValue.ToString(); ;
                }
                
                // Method
                if (((MainWindow)System.Windows.Application.Current.MainWindow).HttpMethod.SelectedValue != null)
                {
                    request.Method = ((MainWindow)System.Windows.Application.Current.MainWindow).HttpMethod.SelectedValue.ToString();
                }
                
                // URI
                request.URI = ((MainWindow)System.Windows.Application.Current.MainWindow).URI.Text;
                
                // Parameters
                request.Parameters = ParameterFields.GridToParameterDataElement();
                
                // Headers
                request.Headers = HeaderFields.GridToHeaderDataElement();
                
                // Attachment
                request.Attachment = ((MainWindow)System.Windows.Application.Current.MainWindow).AttachmentPath.Text;
                
                // Assembly
                request.Assembly = ((MainWindow)System.Windows.Application.Current.MainWindow).AssemblyPath.Text;
                
                // Type
                if (((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue != null)
                {
                    request.Type = ((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue.ToString();
                }
                
                // Format
                if (((MainWindow)System.Windows.Application.Current.MainWindow).Formats.SelectedValue != null)
                {
                    request.Format = ((MainWindow)System.Windows.Application.Current.MainWindow).Formats.SelectedValue.ToString();
                }
                
                // Body
                request.Body = ((MainWindow)System.Windows.Application.Current.MainWindow).RequestBody.Text;

            // Save object as XML file
            using (Stream stream = File.Open(FilePath, true ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, request);
            }

        }
    }
}
