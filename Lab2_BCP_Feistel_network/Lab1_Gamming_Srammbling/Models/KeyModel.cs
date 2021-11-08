using Newtonsoft.Json;

namespace Lab2_BCP_Feistel_network.Models
{
    public class KeyModel
    {
        [JsonProperty(PropertyName = "Key")]
        public string Key { get; set; }
    }
}
