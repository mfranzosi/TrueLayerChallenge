using PKMN.PokedexService.Application.Interfaces;
using PKMN.PokedexService.Client;
using PokeApiNet;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddClient(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddSingleton<PokeApiClient>();
        services.AddTransient<IPokemonClient, PokemonClient>();
        services.AddAutoMapper(assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
