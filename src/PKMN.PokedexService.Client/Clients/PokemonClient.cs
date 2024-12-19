using AutoMapper;
using PKMN.PokedexService.Application.Interfaces;
using PKMN.PokedexService.Infrastructure.Interfaces;
using PokeApiNet;

namespace PKMN.PokedexService.Infrastructure.Clients
{
    public class PokemonClient(IPokeApiWrapper client, IMapper mapper) : IPokemonClient
    {
        public async Task<Domain.Entities.Pokemon?> GetPokemonByName(string name, CancellationToken cancellationToken = default)
        {
            try
            {
                var pokemonSpecies = await client.GetResourceAsync<PokemonSpecies>(name, cancellationToken);

                return mapper.Map<Domain.Entities.Pokemon>(pokemonSpecies);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }

                throw;
            }
        }
    }
}
