using PKMN.PokedexService.Infrastructure.Interfaces;
using PokeApiNet;

namespace PKMN.PokedexService.Infrastructure.Wrappers
{
    public class PokeApiWrapper(PokeApiClient client) : IPokeApiWrapper
    {
        public async Task<T> GetResourceAsync<T>(string name, CancellationToken cancellationToken = default) where T : NamedApiResource
        {
            return await client.GetResourceAsync<T>(name, cancellationToken);
        }
    }
}
