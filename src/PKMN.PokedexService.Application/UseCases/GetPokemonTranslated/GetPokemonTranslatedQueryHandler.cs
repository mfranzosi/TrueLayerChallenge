using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PKMN.PokedexService.Application.Dtos;
using PKMN.PokedexService.Application.Interfaces;
using PKMN.PokedexService.Domain.Entities;

namespace PKMN.PokedexService.Application.UseCases.GetPokemonTranslated;

public class GetPokemonTranslatedQueryHandler(
    IPokemonClient pokemonClient,
    IYodaTranslationClient yodaTranslationClient,
    IShakespeareTranslationClient shakespeareTranslationClient,
    IMapper mapper,
    ILogger<GetPokemonTranslatedQueryHandler> logger) : IRequestHandler<GetPokemonTranslatedQuery, PokemonDto?>
{
    public async Task<PokemonDto?> Handle(GetPokemonTranslatedQuery request, CancellationToken cancellationToken = default)
    {
        var pokemon = await pokemonClient.GetPokemonByName(
            request.Name,
            cancellationToken);

        if (pokemon is null)
        {
            return null;
        }

        var translatedDescription = await GetTranslatedDescription(pokemon);
        if (!string.IsNullOrWhiteSpace(translatedDescription))
        {
            pokemon.Description = translatedDescription;
        }

        var result = mapper.Map<PokemonDto>(pokemon);
        return result;
    }

    private async Task<string?> GetTranslatedDescription(Pokemon pokemon)
    {
        string? translatedDescription = null;

        try
        {
            if (ShouldUseYodaTranslation(pokemon))
            {
                translatedDescription = await yodaTranslationClient.GetTranslation(pokemon.Description);
            }
            else
            {
                translatedDescription = await shakespeareTranslationClient.GetTranslation(pokemon.Description);
            }
        }
        catch (Exception exception)
        {
            // Set this LogLevel as Warning, since we consider this case as unexpected.
            // This could happen for several reasons, like Translation Service being down, exceeded number of requests,
            // or a too long/invalid text being sent.
            logger.Log(LogLevel.Warning, exception, "Translation for description '{description}' failed", pokemon.Description);
        }

        return translatedDescription;
    }

    private static bool ShouldUseYodaTranslation(Pokemon pokemon) =>
        pokemon.IsLegendary || string.Equals(pokemon.Habitat, "cave", StringComparison.InvariantCultureIgnoreCase);
}
