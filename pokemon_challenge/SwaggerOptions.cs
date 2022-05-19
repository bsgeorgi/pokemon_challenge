using System.Diagnostics.CodeAnalysis;

namespace pokemon_challenge
{
    [ExcludeFromCodeCoverage]
    public class SwaggerOptions
    {
        public string JsonRoute { get; set; }

        public string Description { get; set; }

        public string UIEndpoint { get; set; }
    }
}
