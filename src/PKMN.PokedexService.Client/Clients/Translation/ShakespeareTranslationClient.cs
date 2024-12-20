using Microsoft.Extensions.Options;
using PKMN.PokedexService.Application.Interfaces;
using PKMN.PokedexService.Application.Options;
using PKMN.PokedexService.Infrastructure.Interfaces;

namespace PKMN.PokedexService.Infrastructure.Clients.Translation;

public class ShakespeareTranslationClient(IHttpClientWrapper client, IOptions<TranslationApiOptions> options)
    : BaseTranslationClient(client, options.Value.ShakespeareTranslationApiURI), IShakespeareTranslationClient;
