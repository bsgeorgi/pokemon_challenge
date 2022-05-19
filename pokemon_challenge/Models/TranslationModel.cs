using System.Diagnostics.CodeAnalysis;

namespace pokemon_challenge.Models
{
    [ExcludeFromCodeCoverage]
    public class TranslationModel
    {
        public Success success { get; set; }
        public Contents contents { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Contents
    {
        public string translated { get; set; }
        public string text { get; set; }
        public string translation { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Success
    {
        public int total { get; set; }
    }
}
