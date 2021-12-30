using Newtonsoft.Json;

namespace Lab1_Gamming_Srammbling.Models
{
    public class TextModel
    {
        [JsonProperty(PropertyName = "Text")]
        public string Text { get; set; }
    }
}
