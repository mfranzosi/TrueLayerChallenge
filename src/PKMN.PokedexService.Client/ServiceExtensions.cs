using PKMN.PokedexService.Application.Interfaces;
using PKMN.PokedexService.Infrastructure.Interfaces;
using PKMN.PokedexService.Infrastructure.Wrappers;
using PokeApiNet;
using System.Reflection;
using PKMN.PokedexService.Infrastructure.Clients.Pokemon;
using PKMN.PokedexService.Infrastructure.Clients.Translation;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddClient(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddSingleton<HttpClient>();
        services.AddSingleton<PokeApiClient>();
        services.AddTransient<IPokeApiWrapper, PokeApiWrapper>();
        services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();
        services.AddTransient<IPokemonClient, PokemonClient>();
        services.AddTransient<IShakespeareTranslationClient, ShakespeareTranslationClient>();
        services.AddTransient<IYodaTranslationClient, YodaTranslationClient>();
        services.AddAutoMapper(assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
