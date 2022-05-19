using pokemon_challenge.Models;
using System.Threading.Tasks;

namespace pokemon_challenge.Services
{
    public interface IPokemonService
    {
        Task<FormattedPokemonModel> GetBasicPokemonAsync(string pokemonName);

        Task<FormattedPokemonModel> GetTranslatedPokemonAsync(string pokemonName);
    }
}