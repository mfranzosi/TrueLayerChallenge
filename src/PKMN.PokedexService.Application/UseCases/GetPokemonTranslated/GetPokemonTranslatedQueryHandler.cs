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

        await TryTranslateDescription(pokemon);

        var result = mapper.Map<PokemonDto>(pokemon);
        return result;
    }

    private async Task TryTranslateDescription(Pokemon pokemon)
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

            if (string.IsNullOrWhiteSpace(translatedDescription)) { return; }
            pokemon.Description = translatedDescription;
        }
        catch (Exception exception)
        {
            logger.Log(LogLevel.Warning, "Translation for description '{description}' failed: {message}", pokemon.Description, exception);
        }
    }

    private bool ShouldUseYodaTranslation(Pokemon pokemon) =>
        pokemon.IsLegendary || string.Equals(pokemon.Habitat, "cave", StringComparison.InvariantCultureIgnoreCase);
}
