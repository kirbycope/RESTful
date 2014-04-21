using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTful
{
    class HTTPVerbs
    {
        private static List<string> _HttpVerbList;
        public static List<string> HTTPVerbsList
        {
            get
            {
                if (_HttpVerbList == null)
                {
                    // Create the List
                    _HttpVerbList = new List<string>();
                    _HttpVerbList.Add("CONNECT");
                    _HttpVerbList.Add("COPY");
                    _HttpVerbList.Add("DELETE");
                    _HttpVerbList.Add("GET");
                    _HttpVerbList.Add("HEAD");
                    _HttpVerbList.Add("LOCK");
                    _HttpVerbList.Add("MKCOL");
                    _HttpVerbList.Add("MOVE");
                    _HttpVerbList.Add("OPTIONS");
                    _HttpVerbList.Add("PATCH");
                    _HttpVerbList.Add("POST");
                    _HttpVerbList.Add("PROPFIND");
                    _HttpVerbList.Add("PROPPATCH");
                    _HttpVerbList.Add("PUT");
                    _HttpVerbList.Add("SEARCH");
                    _HttpVerbList.Add("TRACE");
                    _HttpVerbList.Add("UNLOCK");
                }

                return _HttpVerbList;
            }
        }
    }
}