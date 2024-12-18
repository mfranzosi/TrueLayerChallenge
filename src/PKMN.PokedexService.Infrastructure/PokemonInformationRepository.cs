using PKMN.PokedexService.Application.Interfaces;
using PKMN.PokedexService.Domain;

namespace PKMN.PokedexService.Infrastructure
{
    public class PokemonInformationRepository : IPokemonInformationRepository
    {

        public Task<PokemonInformation?> GetPokemonInformationByName(string name, CancellationToken cancellationToken = default)
        {



        }
    }
}
