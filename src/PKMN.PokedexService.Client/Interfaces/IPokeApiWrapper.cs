using PokeApiNet;

namespace PKMN.PokedexService.Infrastructure.Interfaces;

public interface IPokeApiWrapper
{
    Task<T> GetResourceAsync<T>(string name, CancellationToken cancellationToken = default) where T : NamedApiResource;
}
