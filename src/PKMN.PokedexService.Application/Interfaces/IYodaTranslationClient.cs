namespace PKMN.PokedexService.Application.Interfaces;

public interface IYodaTranslationClient
{
    Task<string?> GetTranslation(
        string text,
        CancellationToken cancellationToken = default);
}
