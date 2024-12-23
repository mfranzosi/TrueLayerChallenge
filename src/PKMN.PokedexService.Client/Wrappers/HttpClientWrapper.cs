using PKMN.PokedexService.Infrastructure.Interfaces;

namespace PKMN.PokedexService.Infrastructure.Wrappers;

/// <summary>
/// This class is a wrapper around the <see cref="HttpClient"> class, used for mocking purposes.
/// </summary>
/// <param name="pokeApiClient">The client to wrap.</param>
public class HttpClientWrapper(HttpClient client) : IHttpClientWrapper
{
    /// <summary>
    /// Send a POST request with a cancellation token as an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <param name="content">The HTTP request content sent to the server.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task object representing the asynchronous operation.</returns>
    public async Task<HttpResponseMessage> PostAsync(
        string? requestUri,
        HttpContent? content,
        CancellationToken cancellationToken = default) 
        => await client.PostAsync(requestUri, content, cancellationToken);
}
