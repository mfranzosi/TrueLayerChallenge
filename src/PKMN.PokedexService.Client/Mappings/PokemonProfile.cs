using AutoMapper;
using PKMN.PokedexService.Infrastructure.Resolvers;
using PKMN.PokedexService.Domain.Entities;

namespace PKMN.PokedexService.Infrastructure.Mappings;

public class PokemonProfile : Profile
{
    public PokemonProfile()
    {
        CreateMap<PokeApiNet.PokemonSpecies, Pokemon>()
            .ForMember(entity => entity.Habitat, options => options.MapFrom<HabitatResolver>())
            .ForMember(entity => entity.Description, options => options.MapFrom<DescriptionResolver>());
    }
}
