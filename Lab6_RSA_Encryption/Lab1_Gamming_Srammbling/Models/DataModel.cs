using Newtonsoft.Json;

namespace Lab1_Gamming_Srammbling.Models
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
