namespace PKMN.PokedexService.Infrastructure.Interfaces
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> PostAsync(string? requestUri, HttpContent? content);
    }
}
