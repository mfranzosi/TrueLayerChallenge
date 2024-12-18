using AutoMapper;
using PKMN.PokedexService.Domain.Entities;

namespace PKMN.PokedexService.Client.Resolvers;

public class DescriptionResolver : IValueResolver<PokeApiNet.PokemonSpecies, Pokemon, string>
{
    public string Resolve(
        PokeApiNet.PokemonSpecies source,
        Pokemon destination,
        string destMember,
        ResolutionContext context) => source.FlavorTextEntries
                // we use First since we expect at least one description in English for every Pokemon
                .First(entry => entry.Language.Name.Equals("en", StringComparison.InvariantCultureIgnoreCase))
                .FlavorText;
}
