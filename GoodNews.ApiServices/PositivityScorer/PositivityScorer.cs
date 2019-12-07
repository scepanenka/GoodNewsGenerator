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
        
        private static volatile PositivityScorer _instance;
        private static readonly object SyncRoot = new Object();
        
        public static PositivityScorer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new PositivityScorer();
                    }
                }

                return _instance;
            }
        }


        public async Task<float> GetIndexPositivity(string articleText)
        {
            var _words = GetBaseDictionary();

            float result = 0;
            int summResult = 0;
            int countResult = 0;

            var jsonLemma = await GetLemmasFromArticle(articleText);


            var dictionary = GetWordsDictionary(jsonLemma);

            foreach (var word in dictionary.Keys)
            {
                if (_words.ContainsKey(word))
                {
                    summResult += Convert.ToInt32(_words[word]) * dictionary[word];
                    countResult += dictionary[word];
                }
            }

            result = (float)(summResult / countResult);

            return result;
        }

        private async Task<string> GetLemmasFromArticle(string input)
        {
            var matches = Regex.Matches(input, @"\b[а-яА-Я]{2,}\b");
            input = String.Join(" ", matches.Cast<Match>().Select(m => m.Value));
            input = Regex.Replace(input, @"\s+", " ");
            input = input.ToLower();
            using (var client = new HttpClient())
            {
                // client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

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

        public Dictionary<string, string> GetBaseDictionary()
        {
            {
                try
                {
                    var fileData = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\.." + "\\.." + "\\.." + "\\.." + @"\GoodNews.ApiServices\PositivityScorer\AFINN-ru.json");
                    var baseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(fileData);

                    return baseDictionary;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }

        public Dictionary<string, int> GetWordsDictionary(string jsonLemma)
        {
            Dictionary<string, int> wordsDictionary = new Dictionary<string, int>();

            try
            {
                var dictionary = JsonConvert.DeserializeObject<List<JsonLemma>>(jsonLemma);
                var annotation = dictionary[0].annotations;

                foreach (var lemma in annotation.Lemmas)
                {
                    if (lemma.Value != "")
                    {
                        if (wordsDictionary.ContainsKey(lemma.Value))
                        {
                            wordsDictionary[lemma.Value] += 1;
                        }
                        else
                        {
                            wordsDictionary[lemma.Value] = 1;
                        }
                    }
                }

                return wordsDictionary;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
