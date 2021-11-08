using Newtonsoft.Json;

namespace Lab2_BCP_Feistel_network.Models
{
    public class DataModel
    {
        [JsonProperty(PropertyName = "text")]
        public string text { get; set; }

        [JsonProperty(PropertyName = "key")]
        public string key { get; set; }

        [JsonProperty(PropertyName = "chiphr")]
        public string chiphr { get; set; }

    }
}
