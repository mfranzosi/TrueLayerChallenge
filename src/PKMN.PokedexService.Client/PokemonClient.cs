using AutoMapper;
using PKMN.PokedexService.Application.Interfaces;
using PokeApiNet;

namespace PKMN.PokedexService.Client
{
    public class PokemonClient(PokeApiClient client, IMapper mapper) : IPokemonClient
    {
        public async Task<Domain.Entities.Pokemon?> GetPokemonByName(string name, CancellationToken cancellationToken = default)
        {
            // var pokemon = await client.GetResourceAsync<PokeApiNet.Pokemon>(name, cancellationToken);

            var pokemonSpecies = await client.GetResourceAsync<PokemonSpecies>(name, cancellationToken);

            return mapper.Map<Domain.Entities.Pokemon>(pokemonSpecies);
        }
    }
}
