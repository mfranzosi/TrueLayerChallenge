using AutoMapper;
using PKMN.PokedexService.Application.Dtos;
using PKMN.PokedexService.Domain.Entities;

namespace PKMN.PokedexService.Application.Mappings
{
    public class PokemonProfile : Profile
    {
        public PokemonProfile()
        {
            CreateMap<Pokemon, PokemonDto>();
        }
    }
}
