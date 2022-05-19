using Newtonsoft.Json;
using pokemon_challenge.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace pokemon_challenge.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private const string ApiBasePath = "https://api.funtranslations.com/translate";
        private readonly string[] _allowedTranslations = new string[] { "shakespeare", "yoda" };

        public TranslationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TranslationModel> GetTranslationAsync(string text, string translation)
        {
            if (string.IsNullOrEmpty(translation) || !_allowedTranslations.Any(translation.Contains))
            {
                return null;
            }

            var uri = $"{ApiBasePath}/{translation}";
            var httpClient = _httpClientFactory.CreateClient();

            var data = new Dictionary<string, string>
            {
                { "text", text }
            };

            var content = new FormUrlEncodedContent(data);
            var response = await httpClient.PostAsync(uri, content);

            if (!response.IsSuccessStatusCode) return null;

            var responseString = await response.Content.ReadAsStringAsync();
            
            return string.IsNullOrEmpty(responseString)
                ? null
                : JsonConvert.DeserializeObject<TranslationModel>(responseString);
        }
    }
}
