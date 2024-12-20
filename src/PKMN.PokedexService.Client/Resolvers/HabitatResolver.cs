using AutoMapper;
using PKMN.PokedexService.Domain.Entities;

namespace PKMN.PokedexService.Infrastructure.Resolvers;

public class HabitatResolver : IValueResolver<PokeApiNet.PokemonSpecies, Pokemon, string?>
{
    public string? Resolve(
        PokeApiNet.PokemonSpecies source,
        Pokemon destination,
        string? destinationMember,
        ResolutionContext context) => source.Habitat?.Name;
}
