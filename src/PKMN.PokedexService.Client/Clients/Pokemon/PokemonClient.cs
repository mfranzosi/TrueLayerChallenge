using AutoMapper;
using Microsoft.Extensions.Logging;
using PKMN.PokedexService.Application.Interfaces;
using PKMN.PokedexService.Infrastructure.Interfaces;
using PokeApiNet;

namespace PKMN.PokedexService.Infrastructure.Clients.Pokemon;

public class PokemonClient(IPokeApiWrapper client, IMapper mapper, ILogger<PokemonClient> logger) : IPokemonClient
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
            // we consider a Not Found as a common error case. Therefore we handle it by
            // returning a null value instead of raising an exception, minimizing performance impact.
            if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                logger.Log(LogLevel.Information, "Pokemon named {name} not found.", name);
                return null;
            }

            throw;
        }
    }
}
