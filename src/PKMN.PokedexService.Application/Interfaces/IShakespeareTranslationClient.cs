namespace PKMN.PokedexService.Application.Interfaces
{
    public interface IShakespeareTranslationClient
    {
        public Task<string?> GetTranslation(
            string text,
            CancellationToken cancellationToken = default);
    }
}
