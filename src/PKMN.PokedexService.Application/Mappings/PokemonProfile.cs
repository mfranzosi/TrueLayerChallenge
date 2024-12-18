﻿using AutoMapper;
using PKMN.PokedexService.Application.Dtos;
using PKMN.PokedexService.Domain.Entities;

namespace PKMN.PokedexService.Application.Mappings
{
    internal class PokemonProfile : Profile
    {
        public PokemonProfile()
        {
            CreateMap<Pokemon, PokemonDto>();
        }
    }
}