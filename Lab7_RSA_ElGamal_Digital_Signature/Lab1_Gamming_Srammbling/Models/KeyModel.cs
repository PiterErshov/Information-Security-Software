using Newtonsoft.Json;

namespace Lab1_Gamming_Srammbling.Models
{
    public class KeyModel
    {
        [JsonProperty(PropertyName = "Key")]
        public string Key { get; set; }
    }
}
