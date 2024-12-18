using PKMN.PokedexService.Domain.Entities;

namespace PKMN.PokedexService.Application.Interfaces
{
    public interface IPokemonClient
    {
        public Task<Pokemon?> GetPokemonByName(
            string name,
            CancellationToken cancellationToken = default);
    }
}
