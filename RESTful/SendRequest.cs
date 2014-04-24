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

namespace RESTful
{
    class SendRequest
    {
        public static HttpResponseMessage Send()
        {
            // Get inputs from saved settings
            /// Input Settings
            string authenticationMethod = RESTful.Properties.Settings.Default.AuthenticationMethod;
            string version = RESTful.Properties.Settings.Default.Protocol;
            string httpMethod = RESTful.Properties.Settings.Default.Method;
            string baseAddress = RESTful.Properties.Settings.Default.BaseAddress;
            string resource = RESTful.Properties.Settings.Default.Resource;
            string requestUri = String.Format("{0}{1}", baseAddress, resource);
            string attachment = RESTful.Properties.Settings.Default.AttachmentFile;
            string requestHeaders = RESTful.Properties.Settings.Default.RequestHeader;
            string requestBody = RESTful.Properties.Settings.Default.RequestBody;
            /// Import Settings
            string type = RESTful.Properties.Settings.Default.Type;
            string format = RESTful.Properties.Settings.Default.Format;

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
                    foreach (var header in RequestHeaders.StringToDict(requestHeaders)) { request.Headers.Add(header.Key, header.Value); }
                    // Define the HttpContent
                    if ((requestBody != null) && (requestBody != "")) { request.Content = RequestBody.Body(requestBody); }
                    // TODO: Implement Properties
                    //request.Properties = new IDictionary<string,object>();

                try
                {
                    // Execute the SendAsync(HttpRequestMessage)
                    HttpResponseMessage result = Client.client.SendAsync(request).Result;

                    // Return HttpResponseMessage value
                    return result;
                }
                catch
                {
                    HttpResponseMessage result = null;

                    // Return HttpResponseMessage value
                    return result;
                }
            }
            else
            {
                using (FileStream myDoc = File.OpenRead(attachment))
                {
                    // Create the HttpRequest Content
                    MultipartFormDataContent content = new MultipartFormDataContent();

                    // Create and define the object to be passed in new ObjectContent as part of HttpContent
                    object instance = RequestBody.BodyWithFile(requestBody);

                    // Convert the Sample Record Data object to a serialized JSON string
                    string serializedString = JsonConvert.SerializeObject(instance);

                    // URL encode the serialized JSON string
                    string encodedString = Uri.EscapeUriString(serializedString);

                    // Add attachment as FileStream
                    content.Add(new StreamContent(myDoc), "file", encodedString);

                    try
                    {
                        // Create and define the result of the POST
                        Task<HttpResponseMessage> result = Client.client.PostAsync(requestUri, content);

                        // Return HttpResponseMessage's result value
                        return result.Result;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }
    }
}