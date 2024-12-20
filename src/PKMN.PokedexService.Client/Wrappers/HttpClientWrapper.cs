using PKMN.PokedexService.Infrastructure.Interfaces;
using PokeApiNet;

namespace PKMN.PokedexService.Infrastructure.Wrappers;

/// <summary>
/// This class is a wrapper around the <see cref="HttpClient"> class, used for mocking purposes.
/// </summary>
/// <param name="pokeApiClient">The client to wrap.</param>
public class HttpClientWrapper(HttpClient client) : IHttpClientWrapper
{
    public async Task<HttpResponseMessage> PostAsync(
        string? requestUri,
        HttpContent? content,
        CancellationToken cancellationToken = default) 
        => await client.PostAsync(requestUri, content, cancellationToken);
}
