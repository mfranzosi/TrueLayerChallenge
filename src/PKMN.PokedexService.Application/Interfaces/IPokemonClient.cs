using PKMN.PokedexService.Domain.Entities;

namespace PKMN.PokedexService.Application.Interfaces;

public interface IPokemonClient
{
    Task<Pokemon?> GetPokemonByName(
        string name,
        CancellationToken cancellationToken = default);
}
