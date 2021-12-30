using Newtonsoft.Json;

namespace Lab2_BCP_Feistel_network.Models
{
    public class ChiphrModel
    {
        [JsonProperty(PropertyName = "Chiphr")]
        public string Chiphr { get; set; }
    }
}
