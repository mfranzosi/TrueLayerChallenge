using Newtonsoft.Json.Linq;
using PKMN.PokedexService.Infrastructure.Clients.Translation.Dtos;
using PKMN.PokedexService.Infrastructure.Interfaces;
using System.Text.Json;

namespace PKMN.PokedexService.Infrastructure.Clients.Translation;

/// <summary>
/// Base class for implementing Translation Clients and avoid code duplications, since the only difference between translators is the URI.
/// Each Client has a different concrete class, so the code is already set up for the implementations to be eventually different in the future.
/// </summary>
/// <param name="client">The HTTP Client tha will perform the underlying request.</param>
/// <param name="uri">The URI used by Client.</param>
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

        var jsonContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<TranslationResponse>(jsonContent)?
            .TranslationResponseContent?
            .TranslatedText;

        // The one below is an alternative approach, that of course is not optimal for performances,
        // but it would spare the creation of deserialization classes TranslationResponse and TranslationResponseContent.

        //var jsonContent = await response.Content.ReadAsStringAsync(cancellationToken);
        //dynamic dynamicContent = JObject.Parse(jsonContent);
        //return dynamicContent?.contents?.translated?.ToString();
    }
}
