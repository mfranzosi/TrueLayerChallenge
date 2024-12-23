using PKMN.PokedexService.Domain.Entities;

namespace PKMN.PokedexService.Application.Interfaces;

public interface IPokemonClient
{
    /// <summary>
    /// Gets a Pokémon by its name, and returns null if it's not found.
    /// </summary>
    /// <param name="name">The name of the Pokémon.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The Pokèmon object, or null if it's not found.</returns>
    Task<Pokemon?> GetPokemonByName(
        string name,
        CancellationToken cancellationToken = default);
}
