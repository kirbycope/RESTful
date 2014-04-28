using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTful
{
    class EncodingFormats
    {
        private static List<string> _FormatList;
        public static List<string> FormatList
        {
            get
            {
                if (_FormatList == null)
                {
                    // Create the List
                    _FormatList = new List<string>();
                    _FormatList.Add("JSON");
                    _FormatList.Add("XML");
                }

                return _FormatList;
            }
        }
    }
}
