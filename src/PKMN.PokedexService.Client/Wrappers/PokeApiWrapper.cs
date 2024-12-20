using PKMN.PokedexService.Infrastructure.Interfaces;
using PokeApiNet;

namespace PKMN.PokedexService.Infrastructure.Wrappers;

/// <summary>
/// This class is a wrapper around the <see cref="PokeApiClient">, used for mocking purposes.
/// </summary>
/// <param name="pokeApiClient">The client to wrap.</param>
public class PokeApiWrapper(PokeApiClient pokeApiClient) : IPokeApiWrapper
{
    public async Task<T> GetResourceAsync<T>(string name, CancellationToken cancellationToken = default) where T : NamedApiResource
    {
        return await pokeApiClient.GetResourceAsync<T>(name, cancellationToken);
    }
}
