using Newtonsoft.Json.Linq;
using PKMN.PokedexService.Application.Interfaces;
using PKMN.PokedexService.Infrastructure.Interfaces;

namespace PKMN.PokedexService.Infrastructure.Clients
{
    public class YodaTranslationClient(IHttpClientWrapper client) : IYodaTranslationClient
    {
        const string URI = "https://api.funtranslations.com/translate/yoda.json";

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
