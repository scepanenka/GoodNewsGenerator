using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GoodNews.Core;
using Newtonsoft.Json;

namespace AffinRuService
{
    public class AffinRuDictionary : IAffinService
    {

        public async Task<Dictionary<string, int>> GetDictionary()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            using (StreamReader streamReader = new StreamReader("src/AFINN-ru.json", System.Text.Encoding.UTF8))
            {
                var affinData = await streamReader.ReadToEndAsync();
                dictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(affinData);

            }

            return dictionary;
        }
    }
}
