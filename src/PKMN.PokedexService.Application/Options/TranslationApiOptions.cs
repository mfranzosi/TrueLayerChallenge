namespace PKMN.PokedexService.Application.Options;

public class TranslationApiOptions
{
    public const string TranslationApi = nameof(TranslationApi);

    public required string YodaTranslationApiURI { get; init; }

    public required string ShakespeareTranslationApiURI { get; init; }
}
