using AutoMapper;
using PKMN.PokedexService.Domain.Entities;

namespace PKMN.PokedexService.Infrastructure.Resolvers;

public class DescriptionResolver : IValueResolver<PokeApiNet.PokemonSpecies, Pokemon, string>
{
    public string Resolve(
        PokeApiNet.PokemonSpecies source,
        Pokemon destination,
        string destinationMember,
        ResolutionContext context) => source.FlavorTextEntries
                // we use First since we expect at least one description in English for every Pokémon
                .First(entry => entry.Language.Name.Equals("en", StringComparison.InvariantCultureIgnoreCase))
                .FlavorText;
}
