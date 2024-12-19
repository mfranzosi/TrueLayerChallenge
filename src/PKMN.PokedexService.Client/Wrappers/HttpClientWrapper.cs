using PKMN.PokedexService.Infrastructure.Interfaces;
using PokeApiNet;

namespace PKMN.PokedexService.Infrastructure.Wrappers
{
    public class HttpClientWrapper(HttpClient client) : IHttpClientWrapper
    {
        public async Task<HttpResponseMessage> PostAsync(string? requestUri, HttpContent? content)
            => await client.PostAsync(requestUri, content);
    }
}
