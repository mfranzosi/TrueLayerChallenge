using Newtonsoft.Json.Linq;
using PKMN.PokedexService.Infrastructure.Interfaces;

namespace PKMN.PokedexService.Infrastructure.Clients.Translation;

public abstract class BaseTranslationClient(IHttpClientWrapper client, string uri)
{
    public virtual async Task<string?> GetTranslation(string text, CancellationToken cancellationToken = default)
    {
        var values = new Dictionary<string, string> { { "text", text } };
        var response = await client.PostAsync(uri, new FormUrlEncodedContent(values), cancellationToken);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return null;
        }

        // this approach is of course not optimal for performances,
        // but it avoids the creation of deserialization classes.
        var jsonContent = await response.Content.ReadAsStringAsync(cancellationToken);
        dynamic dynamicContent = JObject.Parse(jsonContent);

        return dynamicContent?.contents?.translated?.ToString();
    }
}
