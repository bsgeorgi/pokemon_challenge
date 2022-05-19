using System.Diagnostics.CodeAnalysis;
using pokemon_challenge.Models;
using System.Text.RegularExpressions;

namespace pokemon_challenge.Extensions
{
    public static class PokemonExtension
    {
        [ExcludeFromCodeCoverage]
        public static FormattedPokemonModel Beautified(this PokemonModel pokemonModel)
        {
            var description = pokemonModel.flavor_text_entries.Count > 0
                                ? pokemonModel.flavor_text_entries.Find(i => i.language.name == "en")?.flavor_text
                                : "";

            var filteredDescription = string.IsNullOrEmpty(description)
                                        ? string.Empty
                                        : Regex.Replace(description, "[^a-zA-Z0-9_.]+", " ", RegexOptions.Compiled);

            var habitat = pokemonModel.habitat != null
                            ? pokemonModel.habitat.name.Trim()
                            : string.Empty;

            return new FormattedPokemonModel
            {
                name = pokemonModel.name,
                description = filteredDescription,
                habitat = habitat,
                isLegendary = pokemonModel.is_legendary
            };
        }
    }
}
