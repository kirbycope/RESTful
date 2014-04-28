using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTful
{
    class HTTPMethods
    {
        private static List<string> _HttpMethodsList;
        public static List<string> HTTPMethodsList
        {
            get
            {
                if (_HttpMethodsList == null)
                {
                    // Create the List
                    _HttpMethodsList = new List<string>();
                    _HttpMethodsList.Add("CONNECT");
                    _HttpMethodsList.Add("COPY");
                    _HttpMethodsList.Add("DELETE");
                    _HttpMethodsList.Add("GET");
                    _HttpMethodsList.Add("HEAD");
                    _HttpMethodsList.Add("LOCK");
                    _HttpMethodsList.Add("MKCOL");
                    _HttpMethodsList.Add("MOVE");
                    _HttpMethodsList.Add("OPTIONS");
                    _HttpMethodsList.Add("PATCH");
                    _HttpMethodsList.Add("POST");
                    _HttpMethodsList.Add("PROPFIND");
                    _HttpMethodsList.Add("PROPPATCH");
                    _HttpMethodsList.Add("PUT");
                    _HttpMethodsList.Add("SEARCH");
                    _HttpMethodsList.Add("TRACE");
                    _HttpMethodsList.Add("UNLOCK");
                }

                return _HttpMethodsList;
            }
        }
    }
}