using Newtonsoft.Json.Linq;
using PKMN.PokedexService.Application.Interfaces;

namespace PKMN.PokedexService.Client.Clients
{
    public class YodaTranslationClient(HttpClient client) : IYodaTranslationClient
    {
        const string URI = "https://api.funtranslations.com/translate/yoda.json";

        public async Task<string?> GetTranslation(string text, CancellationToken cancellationToken = default)
        {
            var values = new Dictionary<string, string> { { "text", text } };
            var response = await client.PostAsync(URI, new FormUrlEncodedContent(values));
            var jsonContent = await response.Content.ReadAsStringAsync();
            dynamic dynamicContent = JObject.Parse(jsonContent);

            return dynamicContent?.contents?.translated?.ToString();
        }
    }
}
