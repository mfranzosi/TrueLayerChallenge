using System.Text.Json.Serialization;

namespace PKMN.PokedexService.Infrastructure.Clients.Translation.Dtos
{
    internal class TranslationResponse
    {
        [JsonPropertyName("contents")]
        public required TranslationResponseContent TranslationResponseContent { get; set; }
    }
}
