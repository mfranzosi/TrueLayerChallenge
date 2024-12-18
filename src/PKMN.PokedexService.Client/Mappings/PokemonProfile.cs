using AutoMapper;
using PKMN.PokedexService.Client.Resolvers;
using PKMN.PokedexService.Domain.Entities;

namespace PKMN.PokedexService.Application.Mappings
{
    internal class PokemonProfile : Profile
    {
        public PokemonProfile()
        {
            CreateMap<PokeApiNet.PokemonSpecies, Pokemon>()
                .ForMember(entity => entity.Habitat, options => options.MapFrom<HabitatResolver>())
                .ForMember(entity => entity.Description, options => options.MapFrom<DescriptionResolver>());
        }
    }
}
