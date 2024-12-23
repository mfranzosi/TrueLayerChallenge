using AutoBogus;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PKMN.PokedexService.Infrastructure.Clients.Pokemon;
using PKMN.PokedexService.Infrastructure.Interfaces;
using PKMN.PokedexService.Infrastructure.Mappings;
using PokeApiNet;

namespace PKMN.PokedexService.Infrastructure.Tests.Clients.Pokemon;

public class PokemonClientTests
{
    private readonly IMapper _mapper;
    private readonly PokemonClient _pokemonClient;
    private readonly IPokeApiWrapper _pokeApiClient;
    private readonly ILogger<PokemonClient> _logger;

    public PokemonClientTests()
    {
        _pokeApiClient = Substitute.For<IPokeApiWrapper>();
        _logger = Substitute.For<ILogger<PokemonClient>>();

        var mapperConfiguration = new MapperConfiguration(cfg =>
           cfg.AddProfile(new PokemonProfile()));
        _mapper = mapperConfiguration.CreateMapper();
        _pokemonClient = new PokemonClient(_pokeApiClient, _mapper, _logger);
    }

    [Fact]
    public async Task Returns_Pokemon_Successfully()
    {
        //Arrange
        var name = AutoFaker.Generate<string>();

        var flavourTextEntries = new AutoFaker<PokemonSpeciesFlavorTexts>()
            .RuleFor(fake => fake.Language, _ => new NamedApiResource<Language>() { Name = "it" })
            .Generate(5);

        var englishFlavorTextEntry = flavourTextEntries.Last();
        englishFlavorTextEntry.Language.Name = "en";

        var pokemonSpecies = new AutoFaker<PokemonSpecies>()
           .RuleFor(fake => fake.FlavorTextEntries, _ => flavourTextEntries)
           .Generate();

        _pokeApiClient.GetResourceAsync<PokemonSpecies>(name)
            .Returns(pokemonSpecies);

        //Act
        var result = await _pokemonClient.GetPokemonByName(name);

        //Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be(pokemonSpecies.Name);
        result.IsLegendary.Should().Be(pokemonSpecies.IsLegendary);
        result.Habitat.Should().Be(pokemonSpecies.Habitat.Name);
        result.Description.Should().Be(englishFlavorTextEntry.FlavorText);
    }

    [Fact]
    public async Task Returns_Null_When_NotFound()
    {
        //Arrange
        var name = AutoFaker.Generate<string>();

        _pokeApiClient.GetResourceAsync<PokemonSpecies>(name)
            .ThrowsAsync(new HttpRequestException("Not found error message", null, System.Net.HttpStatusCode.NotFound));

        //Act
        var result = await _pokemonClient.GetPokemonByName(name);

        //Assert
        result.Should().BeNull();
    }
}
