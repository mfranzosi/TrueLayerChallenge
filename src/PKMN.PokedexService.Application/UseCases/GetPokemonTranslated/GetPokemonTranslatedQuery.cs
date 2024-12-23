using MediatR;
using PKMN.PokedexService.Application.Dtos;

namespace PKMN.PokedexService.Application.UseCases.GetPokemonTranslated;

public record GetPokemonTranslatedQuery : IRequest<PokemonDto?>
{
    public required string Name { get; init; }
}
