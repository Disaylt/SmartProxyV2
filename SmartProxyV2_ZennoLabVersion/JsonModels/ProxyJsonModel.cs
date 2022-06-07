using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartProxyV2_ZennoLabVersion.JsonModels
{
    internal class ProxyJsonModel
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
