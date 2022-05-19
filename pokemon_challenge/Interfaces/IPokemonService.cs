using System.Threading.Tasks;
using pokemon_challenge.Models;

namespace pokemon_challenge.Interfaces
{
    public interface IPokemonService
    {
        Task<PokemonModel> RetrievePokemonDataAsync(string pokemonName);

        Task<FormattedPokemonModel> GetBasicPokemonAsync(string pokemonName);

        Task<FormattedPokemonModel> GetTranslatedPokemonAsync(string pokemonName);
    }
}