using Newtonsoft.Json;

namespace Lab1_Gamming_Srammbling.Models
{
    public class ScramModel
    {
        [JsonProperty(PropertyName = "Scram")]
        public string Scram { get; set; }
    }
}
