using AutoMapper;
using MediatR;
using PKMN.PokedexService.Application.Dtos;
using PKMN.PokedexService.Application.Interfaces;

namespace PKMN.PokedexService.Application.UseCases.GetPokemon;

public class GetPokemonQueryHandler(
    IPokemonClient pokemonClient,
    IMapper mapper) : IRequestHandler<GetPokemonQuery, PokemonDto?>
{
    public async Task<PokemonDto?> Handle(GetPokemonQuery request, CancellationToken cancellationToken = default)
    {
        var pokemon = await pokemonClient.GetPokemonByName(
            request.Name,
            cancellationToken);

        var result = mapper.Map<PokemonDto>(pokemon);
        return result;
    }
}
