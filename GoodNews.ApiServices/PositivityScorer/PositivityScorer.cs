using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GoodNews.ApiServices.PositivityScorer.Models;
using GoodNews.Core;
using Newtonsoft.Json;

namespace GoodNews.ApiServices.PositivityScorer
{
    public class PositivityScorer : IPositivityScorer
    {
        public async Task<float> GetIndexPositivity(string articleText)
        {
            var affinDictionary = GetAfinnDictionary();
            var jsonLemma = await GetLemmasFromArticle(articleText);
            var articleWords = GetDictionaryFromResponse(jsonLemma);

            int totalScore = 0;
            int wordsCount = 0;
            foreach (var key in articleWords.Keys)
            {
                if (affinDictionary.ContainsKey(key))
                {
                    totalScore += Convert.ToInt32(affinDictionary[key]) * articleWords[key];
                    wordsCount += articleWords[key];
                }
            }

            float result = (float)totalScore / wordsCount;

            return result;
        }

        private async Task<string> GetLemmasFromArticle(string input)
        {
            input = input.ToLower();
            var wordsCollection = Regex.Matches(input, @"\b[а-я]{3,}\b");
            input = String.Join(" ", wordsCollection.Cast<Match>().Select(m => m.Value));
            using (var client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post,
                    "http://api.ispras.ru/texterra/v1/nlp?targetType=lemma&apikey=de7e616f3ec4bd9b67d7923692a692eddf4478ef")
                )
                {
                    request.Content = new StringContent("[ { \"text\" : \"" + input + "\" } ]",
                        Encoding.UTF8, "application/json");

                    var response = await client.SendAsync(request);
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
            }
        }

        public Dictionary<string, string> GetAfinnDictionary()
        {
            try
            {
                var afinnJson = File.ReadAllText("src/AFINN-ru.json");
                var baseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(afinnJson);

                return baseDictionary;
            }
            catch 
            {
                return null;
            }
        }

        public Dictionary<string, int> GetDictionaryFromResponse(string responseText)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();


            var responseJson = JsonConvert.DeserializeObject<List<JsonLemma>>(responseText);
            var annotation = responseJson[0].Annotations;

            foreach (var item in annotation.Lemmas)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    dictionary[item.Value] = (dictionary.ContainsKey(item.Value)) ? ++dictionary[item.Value] : 1;
                }
            }

            return dictionary;
        }
    }
}
