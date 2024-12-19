namespace PKMN.PokedexService.Application.Interfaces
{
    public interface IShakespeareTranslationClient
    {
        Task<string?> GetTranslation(
            string text,
            CancellationToken cancellationToken = default);
    }
}
