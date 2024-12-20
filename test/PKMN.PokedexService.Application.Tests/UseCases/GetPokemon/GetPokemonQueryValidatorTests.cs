using AutoBogus;
using FluentAssertions;
using PKMN.PokedexService.Application.UseCases.GetPokemon;

namespace PKMN.PokedexService.Application.Tests.UseCases.GetPokemon;

public class GetPokemonQueryValidatorTests
{
    private readonly GetPokemonQueryValidator _getPokemonQueryValidator;

    private const string NotEmptyValidatorName = "NotEmptyValidator";

    private const string NamePropertyName
        = nameof(GetPokemonQuery.Name);

    public GetPokemonQueryValidatorTests()
    {
        _getPokemonQueryValidator = new GetPokemonQueryValidator();
    }

    [Fact]
    public void Should_Validate_True_Because_All_Ok()
    {
        //Arrange
        var query = new GetPokemonQuery
        {
            Name = AutoFaker.Generate<string>()
        };

        //Act
        var result = _getPokemonQueryValidator.Validate(query);

        //Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Should_Validate_False_Because_Name_Is_Null()
    {
        //Arrange
        var query = new GetPokemonQuery
        {
            Name = null!
        };

        //Act
        var result = _getPokemonQueryValidator.Validate(query);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        var first = result.Errors[0];
        first.PropertyName.Should().Be(NamePropertyName);
        first.ErrorCode.Should().Be(NotEmptyValidatorName);
    }
    
    [Fact]
    public void Should_Validate_False_Because_Name_Is_Empty()
    {
        //Arrange
        var query = new GetPokemonQuery
        {
            Name = string.Empty
        };

        //Act
        var result = _getPokemonQueryValidator.Validate(query);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        var first = result.Errors[0];
        first.PropertyName.Should().Be(NamePropertyName);
        first.ErrorCode.Should().Be(NotEmptyValidatorName);
    }

    [Fact]
    public void Should_Validate_False_Because_Name_Is_WhiteSpace()
    {
        //Arrange
        var query = new GetPokemonQuery
        {
            Name = new string(' ', 5)
        };

        //Act
        var result = _getPokemonQueryValidator.Validate(query);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        var first = result.Errors[0];
        first.PropertyName.Should().Be(NamePropertyName);
        first.ErrorCode.Should().Be(NotEmptyValidatorName);
    }
}
