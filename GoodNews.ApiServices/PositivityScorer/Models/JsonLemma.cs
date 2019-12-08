using Newtonsoft.Json;

namespace GoodNews.ApiServices.PositivityScorer.Models
{
    public class JsonLemma
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("annotations")]
        public Annotations Annotations { get; set; }
    }
}
