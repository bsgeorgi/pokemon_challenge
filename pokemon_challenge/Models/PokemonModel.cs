using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace pokemon_challenge.Models
{
    [ExcludeFromCodeCoverage]
    public class PokemonModel
    {
        [JsonIgnore]
        public int id { get; set; }

        public string name { get; set; }

        [JsonIgnore]
        public int order { get; set; }

        [JsonIgnore]
        public int gender_rate { get; set; }

        [JsonIgnore]
        public int capture_rate { get; set; }

        [JsonIgnore]
        public int base_happiness { get; set; }

        [JsonIgnore]
        public bool is_baby { get; set; }

        public bool is_legendary { get; set; }

        [JsonIgnore]
        public bool is_mythical { get; set; }

        [JsonIgnore]
        public int hatch_counter { get; set; }

        [JsonIgnore]
        public bool has_gender_differences { get; set; }

        [JsonIgnore]
        public bool forms_switchable { get; set; }

        [JsonIgnore]
        public GrowthRate growth_rate { get; set; }

        [JsonIgnore]
        public List<PokedexNumber> pokedex_numbers { get; set; }

        [JsonIgnore]
        public List<EggGroup> egg_groups { get; set; }

        [JsonIgnore]
        public Color color { get; set; }

        [JsonIgnore]
        public Shape shape { get; set; }

        [JsonIgnore]
        public EvolvesFromSpecies evolves_from_species { get; set; }

        [JsonIgnore]
        public EvolutionChain evolution_chain { get; set; }

        public Habitat habitat { get; set; }

        [JsonIgnore]
        public Generation generation { get; set; }

        [JsonIgnore]
        public List<Name> names { get; set; }

        public List<FlavorTextEntry> flavor_text_entries { get; set; }

        [JsonIgnore]
        public List<FormDescription> form_descriptions { get; set; }

        [JsonIgnore]
        public List<Genera> genera { get; set; }

        [JsonIgnore]
        public List<Variety> varieties { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class FormattedPokemonModel
    {
        public string name { get; set; }
        
        public string description { get; set; }

        public string habitat { get; set; }

        public bool isLegendary { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Color
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Habitat
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class EggGroup
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class EvolutionChain
    {
        public string url { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class EvolvesFromSpecies
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class FlavorTextEntry
    {
        public string flavor_text { get; set; }
        public Language language { get; set; }
        public Version version { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class FormDescription
    {
        public string description { get; set; }
        public Language language { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Genera
    {
        public string genus { get; set; }
        public Language language { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Generation
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class GrowthRate
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Language
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Name
    {
        public string name { get; set; }
        public Language language { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Pokedex
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class PokedexNumber
    {
        public int entry_number { get; set; }
        public Pokedex pokedex { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Pokemon
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Shape
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Variety
    {
        public bool is_default { get; set; }
        public Pokemon pokemon { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Version
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
