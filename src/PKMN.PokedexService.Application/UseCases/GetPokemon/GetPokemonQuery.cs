using MediatR;
using PKMN.PokedexService.Application.Dtos;

namespace PKMN.PokedexService.Application.UseCases.GetPokemon;

/// <summary>
/// Query object used to get single company by SalesCompanyId
/// </summary>
public record GetPokemonQuery : IRequest<PokemonDto>
{
    public required string Name { get; set; }
}
