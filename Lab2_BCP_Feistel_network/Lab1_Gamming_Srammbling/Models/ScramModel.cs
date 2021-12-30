using Newtonsoft.Json;

namespace Lab2_BCP_Feistel_network.Models
{
    public class ScramModel
    {
        [JsonProperty(PropertyName = "Scram")]
        public string Scram { get; set; }
    }
}
