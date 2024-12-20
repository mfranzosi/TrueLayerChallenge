using AutoBogus;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using PKMN.PokedexService.Api.Controllers;
using PKMN.PokedexService.Application.Dtos;
using PKMN.PokedexService.Application.UseCases.GetPokemon;
using PKMN.PokedexService.Application.UseCases.GetPokemonTranslated;

namespace PKMN.PokedexService.Api.Tests.Controllers;

public class PokemonControllerTests
{
    private readonly IMediator _mediator;
    private readonly PokemonController _pokemonController;
    private readonly ILogger<PokemonController> _logger;

    public PokemonControllerTests()
    {
        _logger = Substitute.For<ILogger<PokemonController>>();
        _mediator = Substitute.For<IMediator>();
        _pokemonController = new PokemonController(_logger, _mediator);
    }

    [Fact]
    public async Task GetPokemon_Returns_Pokemon_Successfully()
    {
        //Arrange
        var pokemonDto = AutoFaker.Generate<PokemonDto>();
        var name = AutoFaker.Generate<string>();

        _mediator
            .Send(
                Arg.Any<GetPokemonQuery>(),
                Arg.Any<CancellationToken>())
            .Returns(pokemonDto);

        // Act
        var actionResult = await _pokemonController.GetPokemon(name);

        //Assert
        var result = actionResult.Result as OkObjectResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(StatusCodes.Status200OK);
        result.Value.Should().NotBeNull().And.Be(pokemonDto);
    }

    [Fact]
    public async Task GetPokemonTranslated_Returns_Pokemon_Successfully()
    {
        //Arrange
        var pokemonDto = AutoFaker.Generate<PokemonDto>();
        var name = AutoFaker.Generate<string>();

        _mediator
            .Send(
                Arg.Any<GetPokemonTranslatedQuery>(),
                Arg.Any<CancellationToken>())
            .Returns(pokemonDto);

        // Act
        var actionResult = await _pokemonController.GetPokemonTranslated(name);

        //Assert
        var result = actionResult.Result as OkObjectResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(StatusCodes.Status200OK);
        result.Value.Should().NotBeNull().And.Be(pokemonDto);
    }

    [Fact]
    public async Task GetPokemon_Returns_NotFound()
    {
        //Arrange
        var name = AutoFaker.Generate<string>();

        _mediator
            .Send(
                Arg.Any<GetPokemonQuery>(),
                Arg.Any<CancellationToken>())
            .ReturnsNull();

        // Act
        var actionResult = await _pokemonController.GetPokemon(name);

        //Assert
        var result = actionResult.Result as NotFoundResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task GetPokemonTranslated_Returns_NotFound()
    {
        //Arrange
        var name = AutoFaker.Generate<string>();

        _mediator
            .Send(
                Arg.Any<GetPokemonTranslatedQuery>(),
                Arg.Any<CancellationToken>())
            .ReturnsNull();

        // Act
        var actionResult = await _pokemonController.GetPokemonTranslated(name);

        //Assert
        var result = actionResult.Result as NotFoundResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}