using Newtonsoft.Json;

namespace LemmatizationService.Models
{
    public class Annotations
    {
        [JsonProperty("lemma")]
        public Lemma[] Lemmas { get; set; }
    }
}
