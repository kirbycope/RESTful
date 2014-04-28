using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTful
{
    class AuthenticationMethods
    {
        private static List<string> _AuthenticationMethodsList;
        public static List<string> AuthenticationMethodsList
        {
            get
            {
                if (_AuthenticationMethodsList == null)
                {
                    // Create the List
                    _AuthenticationMethodsList = new List<string>();
                    _AuthenticationMethodsList.Add("None");
                    _AuthenticationMethodsList.Add("Basic");
                    _AuthenticationMethodsList.Add("Digest");
                    _AuthenticationMethodsList.Add("OAuth1");
                    _AuthenticationMethodsList.Add("OAuth2");
                }

                return _AuthenticationMethodsList;
            }
        }
    }
}