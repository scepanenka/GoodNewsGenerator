using System.Collections.Generic;
using Newtonsoft.Json;

namespace GoodNews.ApiServices.PositivityScorer.Models
{
    public class Annotations
    {
        [JsonProperty("lemma")]
        public Lemma[] Lemmas { get; set; }
    }
}
