using System.Text.Json.Serialization;

namespace PKMN.PokedexService.Infrastructure.Clients.Translation.Dtos
{
    internal class TranslationResponseContent
    {
        [JsonPropertyName("text")]
        public required string Text { get; set; }

        [JsonPropertyName("translated")]
        public required string TranslatedText { get; set; }
    }
}
