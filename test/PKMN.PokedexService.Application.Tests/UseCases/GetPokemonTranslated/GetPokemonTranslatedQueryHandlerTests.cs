using AutoBogus;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PKMN.PokedexService.Application.Interfaces;
using PKMN.PokedexService.Application.Mappings;
using PKMN.PokedexService.Application.UseCases.GetPokemonTranslated;

namespace PKMN.PokedexService.Application.Tests.UseCases.GetPokemon;

public class GetPokemonTranslatedQueryHandlerTests
{
    private readonly IPokemonClient _pokemonClient;
    private readonly IYodaTranslationClient _yodaTranslationClient;
    private readonly IShakespeareTranslationClient _shakespeareTranslationClient;
    private readonly ILogger<GetPokemonTranslatedQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly GetPokemonTranslatedQueryHandler _getPokemonTranslatedQueryHandler;

    public GetPokemonTranslatedQueryHandlerTests()
    {
        _pokemonClient = Substitute.For<IPokemonClient>();
        _yodaTranslationClient = Substitute.For<IYodaTranslationClient>();
        _shakespeareTranslationClient = Substitute.For<IShakespeareTranslationClient>();
        _logger = Substitute.For<ILogger<GetPokemonTranslatedQueryHandler>>();

        var mapperConfiguration = new MapperConfiguration(cfg =>
           cfg.AddProfile(new PokemonProfile()));
        _mapper = mapperConfiguration.CreateMapper();
        _getPokemonTranslatedQueryHandler = new GetPokemonTranslatedQueryHandler(_pokemonClient,
            _yodaTranslationClient,
            _shakespeareTranslationClient,
            _mapper,
            _logger);
    }

    [Theory]
    [InlineData(true, "water")]
    [InlineData(false, "cave")]
    public async Task Returns_Pokemon_Successfully_Using_Yoda_Translation(bool isLegendary, string habitat)
    {
        //Arrange
        var request = new GetPokemonTranslatedQuery()
        {
            Name = AutoFaker.Generate<string>()
        };

        var pokemon = new AutoFaker<Domain.Entities.Pokemon>()
            .RuleFor(fake => fake.IsLegendary, _ => isLegendary)
            .RuleFor(fake => fake.Habitat, _ => habitat)
            .Generate();

        var originalDescription = pokemon.Description;
        var translatedDescription = AutoFaker.Generate<string>();

        _pokemonClient.GetPokemonByName(request.Name)
            .Returns(pokemon);

        _yodaTranslationClient.GetTranslation(pokemon.Description)
            .Returns(translatedDescription);

        //Act
        var result = await _getPokemonTranslatedQueryHandler.Handle(request);

        //Assert
        await _shakespeareTranslationClient.DidNotReceive()
            .GetTranslation(Arg.Any<string>());

        await _yodaTranslationClient.Received(1)
            .GetTranslation(Arg.Any<string>());

        result.Should().NotBeNull();
        result!.Name.Should().Be(pokemon.Name);
        result.Habitat.Should().Be(pokemon.Habitat);
        result.IsLegendary.Should().Be(pokemon.IsLegendary);
        result.Description.Should().NotBe(originalDescription).And.Be(translatedDescription);
    }

    [Fact]
    public async Task Returns_Pokemon_Successfully_Using_Shakespeare_Translation()
    {
        //Arrange
        var request = new GetPokemonTranslatedQuery()
        {
            Name = AutoFaker.Generate<string>()
        };

        var pokemon = new AutoFaker<Domain.Entities.Pokemon>()
            .RuleFor(fake => fake.IsLegendary, _ => false)
            // in this case we use an hardcoded value for Habitat, as "cave" would accidentally lead to a flaky test
            .RuleFor(fake => fake.Habitat, _ => "water")
            .Generate();

        var originalDescription = pokemon.Description;
        var translatedDescription = AutoFaker.Generate<string>();

        _pokemonClient.GetPokemonByName(request.Name)
            .Returns(pokemon);

        _shakespeareTranslationClient.GetTranslation(pokemon.Description)
            .Returns(translatedDescription);

        //Act
        var result = await _getPokemonTranslatedQueryHandler.Handle(request);

        //Assert
        await _yodaTranslationClient.DidNotReceive()
            .GetTranslation(Arg.Any<string>());

        await _shakespeareTranslationClient.Received(1)
            .GetTranslation(Arg.Any<string>());

        result.Should().NotBeNull();
        result!.Name.Should().Be(pokemon.Name);
        result.Habitat.Should().Be(pokemon.Habitat);
        result.IsLegendary.Should().Be(pokemon.IsLegendary);
        result.Description.Should().NotBe(originalDescription).And.Be(translatedDescription);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Returns_Original_Description_When_Translation_Is_Not_Available(bool isLegendary)
    {
        //Arrange
        var request = new GetPokemonTranslatedQuery()
        {
            Name = AutoFaker.Generate<string>()
        };

        var pokemon = new AutoFaker<Domain.Entities.Pokemon>()
            // in this case we test both translations by switching the isLegendary flag
            .RuleFor(fake => fake.IsLegendary, _ => isLegendary)
            .RuleFor(fake => fake.Habitat, _ => "water")
            .Generate();

        var originalDescription = pokemon.Description;

        _pokemonClient.GetPokemonByName(request.Name)
            .Returns(pokemon);

        _shakespeareTranslationClient.GetTranslation(pokemon.Description)
            .ThrowsAsync<Exception>();

        _yodaTranslationClient.GetTranslation(pokemon.Description)
            .ThrowsAsync<Exception>();

        //Act
        var result = await _getPokemonTranslatedQueryHandler.Handle(request);

        //Assert
        result.Should().NotBeNull();
        result!.Description.Should().Be(originalDescription);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task Returns_Original_Description_When_Translation_Is_Empty(bool isLegendary)
    {
        //Arrange
        var request = new GetPokemonTranslatedQuery()
        {
            Name = AutoFaker.Generate<string>()
        };

        var pokemon = new AutoFaker<Domain.Entities.Pokemon>()
            // in this case we test both translations by switching the isLegendary flag
            .RuleFor(fake => fake.IsLegendary, _ => isLegendary)
            .RuleFor(fake => fake.Habitat, _ => "water")
            .Generate();

        var originalDescription = pokemon.Description;

        _pokemonClient.GetPokemonByName(request.Name)
            .Returns(pokemon);

        _shakespeareTranslationClient.GetTranslation(pokemon.Description)
            .Returns(string.Empty);

        _yodaTranslationClient.GetTranslation(pokemon.Description)
            .Returns(string.Empty);

        //Act
        var result = await _getPokemonTranslatedQueryHandler.Handle(request);

        //Assert
        result.Should().NotBeNull();
        result!.Description.Should().Be(originalDescription);
    }
}
