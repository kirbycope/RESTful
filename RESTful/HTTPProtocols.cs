using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTful
{
    class HTTPProtocols
    {
        private static List<string> _HttpProtocolsList;
        public static List<string> HTTPProtocolsList
        {
            get
            {
                if (_HttpProtocolsList == null)
                {
                    // Create the List
                    _HttpProtocolsList = new List<string>();
                    _HttpProtocolsList.Add("0.9");
                    _HttpProtocolsList.Add("1.0");
                    _HttpProtocolsList.Add("1.1");
                    _HttpProtocolsList.Add("1.2");
                    _HttpProtocolsList.Add("2.0");
                }

                return _HttpProtocolsList;
            }
        }
    }
}
