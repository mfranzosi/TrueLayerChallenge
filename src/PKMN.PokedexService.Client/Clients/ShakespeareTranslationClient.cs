using Newtonsoft.Json.Linq;
using PKMN.PokedexService.Application.Interfaces;
using PKMN.PokedexService.Infrastructure.Interfaces;

namespace PKMN.PokedexService.Infrastructure.Clients
{
    public class ShakespeareTranslationClient(IHttpClientWrapper client) : IShakespeareTranslationClient
    {
        const string URI = "https://api.funtranslations.com/translate/shakespeare.json";

        public async Task<string?> GetTranslation(string text, CancellationToken cancellationToken = default)
        {
            var values = new Dictionary<string, string> { { "text", text } };
            var response = await client.PostAsync(URI, new FormUrlEncodedContent(values));

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var jsonContent = await response.Content.ReadAsStringAsync();
            dynamic dynamicContent = JObject.Parse(jsonContent);

            return dynamicContent?.contents?.translated?.ToString();
        }
    }
}
