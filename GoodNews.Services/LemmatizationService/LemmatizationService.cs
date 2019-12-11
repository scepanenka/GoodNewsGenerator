﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GoodNews.Core;
using LemmatizationService.Models;
using Newtonsoft.Json;

namespace LemmatizationService
{
    public class LemmatizationService : ILemmatization
    {
        public async Task<string> GetLemmas(string input)
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

        public Dictionary<string, int> GetDictionaryFromLemmas(string responseText)
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