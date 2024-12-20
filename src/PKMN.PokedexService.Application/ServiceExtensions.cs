using Microsoft.Extensions.Configuration;
using PKMN.PokedexService.Application.Options;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration )
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddOptions<TranslationApiOptions>()
          .Bind(configuration.GetSection(
              TranslationApiOptions.TranslationApi))
          .Validate(options =>
          {
              var validator = new TranslationApiOptionsValidator();
              return validator.Validate(options).IsValid;
          })
          .ValidateOnStart();

        services.AddAutoMapper(assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
