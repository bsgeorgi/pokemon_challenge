using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pokemon_challenge.Models;
using pokemon_challenge.Services;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace pokemon_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IPokemonService _pokemonService;

        public PokemonController(ILogger<PokemonController> logger, IPokemonService pokemonService)
        {
            _logger = logger;
            _pokemonService = pokemonService;
        }

        [HttpGet]
        [Route("{pokemonName}")]
        [ResponseType(typeof(TranslationModel))]
        public async Task<IActionResult> GetBasicPokemon(string pokemonName)
        {
            var pokemonModel = await _pokemonService.GetBasicPokemonAsync(pokemonName);
            if (pokemonModel == null)
            {
                return NotFound();
            }

            return Ok(pokemonModel);
        }

        [HttpGet]
        [Route("translated/{pokemonName}")]
        [ResponseType(typeof(TranslationModel))]
        public async Task<IActionResult> GetTranslatedPokemon(string pokemonName)
        {
            var pokemonModel = await _pokemonService.GetTranslatedPokemonAsync(pokemonName);
            if (pokemonModel == null)
            {
                return NotFound();
            }

            return Ok(pokemonModel);
        }
    }
}
