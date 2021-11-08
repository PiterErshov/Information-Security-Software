using Newtonsoft.Json;

namespace Lab2_BCP_Feistel_network.Models
{
    public class TextModel
    {
        [JsonProperty(PropertyName = "Text")]
        public string Text { get; set; }
    }
}
