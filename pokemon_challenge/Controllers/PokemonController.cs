using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pokemon_challenge.Models;
using System.Threading.Tasks;
using System.Web.Http.Description;
using pokemon_challenge.Interfaces;

namespace pokemon_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
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
