using Newtonsoft.Json;
using System.Numerics;

namespace Lab1_Gamming_Srammbling.Models
{
    public class RSAKeyModel
    {
        [JsonProperty(PropertyName = "Key")]
        public string Key { get; set; }

        [JsonProperty(PropertyName = "N")]
        public string N { get; set; }
    }
}
