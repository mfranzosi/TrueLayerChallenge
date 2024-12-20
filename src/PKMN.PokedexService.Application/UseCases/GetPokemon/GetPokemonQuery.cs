using MediatR;
using PKMN.PokedexService.Application.Dtos;

namespace PKMN.PokedexService.Application.UseCases.GetPokemon;

public record GetPokemonQuery : IRequest<PokemonDto>
{
    public required string Name { get; init; }
}
