using Newtonsoft.Json;

namespace LemmatizationService.Models
{
    public class JsonLemma
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("annotations")]
        public Annotations Annotations { get; set; }
    }
}
