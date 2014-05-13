using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

using System.Reflection;

using System.IO;
using Newtonsoft.Json;
using System.Windows.Controls;

using System.Web;

namespace RESTful
{
    class SendRequest
    {
        public static HttpResponseMessage Send()
        {
            // Get the user inputs
            string authenticationMethod = ((MainWindow)System.Windows.Application.Current.MainWindow).AuthenticationMethod.SelectedValue.ToString();
            string version = ((MainWindow)System.Windows.Application.Current.MainWindow).ProtocolVersion.SelectedValue.ToString();
            string httpMethod = ((MainWindow)System.Windows.Application.Current.MainWindow).HttpMethod.SelectedValue.ToString();
            string requestUri = null;
                if (((MainWindow)System.Windows.Application.Current.MainWindow).URI.Text != null)
                { requestUri = UriParameters.AddToURI(); }
            string attachment = ((MainWindow)System.Windows.Application.Current.MainWindow).AttachmentPath.Text;
            string requestBody = ((MainWindow)System.Windows.Application.Current.MainWindow).RequestBody.Text;
            string type = null;
                if (((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue != null)
                { type = ((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue.ToString(); }
            string format = null;
                if (((MainWindow)System.Windows.Application.Current.MainWindow).Formats.SelectedValue != null)
                { format = ((MainWindow)System.Windows.Application.Current.MainWindow).Formats.SelectedValue.ToString(); }

            // Send Request (no file attachment)
            if ((attachment == null) || (attachment == ""))
            {
                // Create a variable to hold the request
                HttpRequestMessage request = new HttpRequestMessage();

                // Define Protocol Version
                if ((version != null) && (version != "")) { request.Version = new Version(version); }
                // Define the HttpMethod
                if ((httpMethod != null) && (httpMethod != "")) { request.Method = new HttpMethod(httpMethod); }
                // Define the RequestUri
                if ((requestUri != null) && (requestUri != "")) { request.RequestUri = new Uri(requestUri); }
                // Define the Headers
                foreach (var header in RequestHeaders.GridToHeaders()) { request.Headers.Add(header.Key, header.Value); }
                // Define the HttpContent
                if ((requestBody != null) && (requestBody != "")) { request.Content = RequestBody.ToHttpContent(requestBody); }
                // TODO: Implement Properties
                //request.Properties = new IDictionary<string,object>();

                // Create HttPResponse variable
                HttpResponseMessage result = null;

                try
                {
                    // Execute the SendAsync(HttpRequestMessage)
                    result = Client.client.SendAsync(request).Result;

                    // Return HttpResponseMessage value
                    return result;
                }
                catch
                {
                    // Return HttpResponseMessage value
                    return null;
                }
            }
            // Send Request with file attachment
            else
            {
                using (FileStream myDoc = File.OpenRead(attachment))
                {
                    // Create the HttpRequest Content
                    MultipartFormDataContent content = new MultipartFormDataContent();

                    // Convert the Request Body to an object
                    object body = RequestBody.ToObject(requestBody);

                    // Convert the object to a serialized JSON string
                    string bodyJSON = JsonConvert.SerializeObject(body);

                    // URL encode the serialized JSON string
                    string bodyJSONUrlEncoded = HttpUtility.UrlEncode(bodyJSON);

                    // File attachment 1
                    content.Add(new StreamContent(myDoc), "file", bodyJSONUrlEncoded);

                    // Create HttPResponse variable
                    HttpResponseMessage result = new HttpResponseMessage();

                    try
                    {
                        // Create and define the result of the POST
                        result = Client.client.PostAsync(requestUri, content).Result;

                        // Return HttpResponseMessage value
                        return result;
                    }
                    catch
                    {
                        // Return HttpResponseMessage value
                        return null;
                    }
                }

            }
        }

    }
}