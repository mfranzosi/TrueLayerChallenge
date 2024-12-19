namespace PKMN.PokedexService.Application.Interfaces
{
    public interface IYodaTranslationClient
    {
        public Task<string?> GetTranslation(
            string text,
            CancellationToken cancellationToken = default);
    }
}
