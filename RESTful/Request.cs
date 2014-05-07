using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace RESTful
{
    [DataContract]
    [Serializable]
    public sealed class Request
    {
        [DataMember]
        [DisplayName("Authentication")]
        public string Authentication { get; set; }

        [DataMember]
        [DisplayName("AuthenticationFields")]
        public IEnumerable<AuthenticationDataElement> AuthenticationFields { get; set; }

        [DataMember]
        [DisplayName("Protocol")]
        public string Protocol { get; set; }

        [DataMember]
        [DisplayName("Method")]
        public string Method { get; set; }

        [DataMember]
        [DisplayName("URI")]
        public string URI { get; set; }

        [DataMember]
        [DisplayName("Parameters")]
        public IEnumerable<ParameterDataElement> Parameters { get; set; }

        [DataMember]
        [DisplayName("Headers")]
        public IEnumerable<HeaderDataElement> Headers { get; set; }

        [DataMember]
        [DisplayName("Attachment")]
        public string Attachment { get; set; }

        [DataMember]
        [DisplayName("Assembly")]
        public string Assembly { get; set; }

        [DataMember]
        [DisplayName("Type")]
        public string Type { get; set; }

        [DataMember]
        [DisplayName("Format")]
        public string Format { get; set; }

        [DataMember]
        [DisplayName("Body")]
        public string Body { get; set; }
    }
}
