using Newtonsoft.Json;

namespace LemmatizationService.Models
{
    public class Lemma
    {
        [JsonProperty("start")]
        public int Start { get; set; }
        [JsonProperty("end")]
        public int End { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
