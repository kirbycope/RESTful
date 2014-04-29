using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Formatting;

namespace RESTful
{
    class RequestBody
    {
        public static HttpContent ToHttpContent(string requestBody)
        {
            // Create the http content variable
            HttpContent content = null;

            if ((((MainWindow)System.Windows.Application.Current.MainWindow).Formats.SelectedValue.ToString() != null) && (((MainWindow)System.Windows.Application.Current.MainWindow).Formats.SelectedValue.ToString() != ""))
            {
                if (((MainWindow)System.Windows.Application.Current.MainWindow).Formats.SelectedValue.ToString() == "JSON")
                {
                    if ((((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue.ToString() != null) && (((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue.ToString() != "")) // Use reflected type
                    {
                        // Create and define the object to be passed in new ObjectContent as part of HttpContent
                        object instance = ObjectConverter.FromJsonString(requestBody);

                        // Create and define the HttpContent object to be passed in client.SendAsync function as part of a HttpRequestMessage
                        content = new ObjectContent(ReflectedType.Type, instance, new JsonMediaTypeFormatter());
                    }
                    else // Use string type
                    {
                        // Create and define the HttpContent object to be passed in client.SendAsync function as part of a HttpRequestMessage
                        content = new ObjectContent(typeof(string), requestBody, new JsonMediaTypeFormatter());
                    }
                }
                else if (((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue.ToString() == "XML")
                {
                    if ((((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue.ToString() != null) && (((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue.ToString() != "")) // Use reflected type
                    {
                        // Create and define the object to be passed in new ObjectContent as part of HttpContent
                        object instance = ObjectConverter.FromXmlString(requestBody);

                        // Create and define the HttpContent object to be passed in client.SendAsync function as part of a HttpRequestMessage
                        content = new ObjectContent(ReflectedType.Type, instance, new XmlMediaTypeFormatter());
                    }
                    else // Use string type
                    {
                        // Create and define the HttpContent object to be passed in client.SendAsync function as part of a HttpRequestMessage
                        content = new ObjectContent(typeof(string), requestBody, new XmlMediaTypeFormatter());
                    }
                };
            }

            return content;
        }

        public static object ToObject(string requestBody)
        {
            // Create an empty object
            object instance = null;

            if ((requestBody != null) && (requestBody != ""))
            {
                if (((MainWindow)System.Windows.Application.Current.MainWindow).Formats.SelectedValue.ToString() == "JSON")
                {
                    if ((((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue.ToString() != null) && (((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue.ToString() != "")) // Use reflected type
                    {
                        // Create and define the object to be passed in new ObjectContent as part of HttpContent
                        instance = ObjectConverter.FromJsonString(requestBody);
                    }
                    else // Use string type
                    {
                        instance = typeof(string);
                    }
                }
                else if (((MainWindow)System.Windows.Application.Current.MainWindow).Formats.SelectedValue.ToString() == "XML")
                {
                    if ((((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue.ToString() != null) && (((MainWindow)System.Windows.Application.Current.MainWindow).Types.SelectedValue.ToString() != "")) // Use reflected type
                    {
                        // Create and define the object to be passed in new ObjectContent as part of HttpContent
                        instance = ObjectConverter.FromXmlString(requestBody);
                    }
                    else // Use string type
                    {
                        instance = typeof(string);
                    }
                };
            }

            return instance;
        }
    }
}
