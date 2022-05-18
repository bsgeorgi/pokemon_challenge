using Newtonsoft.Json;
using pokemon_challenge.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace pokemon_challenge.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private const string apiBasePath = "https://api.funtranslations.com/translate";

        public TranslationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TranslationObject> GetYodaTranslationObjectAsync(string text)
        {
            var uri = $"{apiBasePath}/yoda";
            var httpClient = _httpClientFactory.CreateClient();

            var data = new Dictionary<string, string>
            {
                { "text", text }
            };

            var content = new FormUrlEncodedContent(data);

            var response = await httpClient.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseString))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<TranslationObject>(responseString);
            }

            return null;
        }
    }
}
