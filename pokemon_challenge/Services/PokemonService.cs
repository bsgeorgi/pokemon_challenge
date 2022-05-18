using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using pokemon_challenge.Extensions;
using pokemon_challenge.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace pokemon_challenge.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly ILogger<PokemonService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITranslationService _translationService;
        private const string apiBasePath = "https://pokeapi.co/api/v2/pokemon-species";

        public PokemonService(ILogger<PokemonService> logger,
            IHttpClientFactory httpClientFactory, ITranslationService translationService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _translationService = translationService;
        }

        private async Task<PokemonModel> GetPokemonDescriptionAsync(string pokemonName)
        {
            var uri = $"{apiBasePath}/{pokemonName}";
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseString))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<PokemonModel>(responseString);
            }

            return null;
        }

        public async Task<FormattedPokemonModel> GetBasicPokemonAsybc(string pokemonName)
        {
            var pokemonModel = await GetPokemonDescriptionAsync(pokemonName);

            if (pokemonModel == null)
            {
                return null;
            }

            return pokemonModel.Beautified();
        }


        public async Task<FormattedPokemonModel> GetTranslatedPokemonAsync(string pokemonName)
        {
            // If the Pokemon’s habitat is cave or it’s a legendary Pokemon then apply the Yoda translation.
            // For all other Pokemon, apply the Shakespeare translation.

            var pokemonModel = await GetPokemonDescriptionAsync(pokemonName);
            TranslationModel translationModel = null;

            if (pokemonModel == null)
            {
                return null;
            }

            var beautiefiedPokemon = pokemonModel.Beautified();

            if (string.Equals(beautiefiedPokemon.habitat, "cave", StringComparison.OrdinalIgnoreCase) ||
                beautiefiedPokemon.isLegendary)
            {
                translationModel = await _translationService.GetTranslationAsync(beautiefiedPokemon.description,
                    "yoda");
            }
            else
            {
                translationModel = await _translationService.GetTranslationAsync(beautiefiedPokemon.description,
                    "shakespeare");
            }

            if (translationModel != null)
            {
                beautiefiedPokemon.description = translationModel.contents.translated;
            }
            else
            {
                // some weird error occurred at this point
                _logger.LogError("Translation model is set to null, unable to translate");
            }

            return beautiefiedPokemon;
        }
    }
}
