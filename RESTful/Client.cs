using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Formatting;

namespace RESTful
{
    class Client
    {
        private static HttpClient _client = null;

        public static HttpClient client
        {
            get
            {
                if (_client == null)
                {
                    // Create the HttpClientHandler
                    HttpClientHandler handler = new HttpClientHandler
                    {
                        UseProxy = false
                    };
                    _client = new HttpClient(handler);
                }
                return _client;
            }
        }
    }
}
