using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace EduBestServiceStub.Lib.NoarkTypes
{

    public partial class PutMessageRequestType
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        [JsonProperty(PropertyName = "id")]
        [XmlIgnore]
        public string Id { get; set; }
    }
}
