using AutoBogus;
using FluentAssertions;
using PKMN.PokedexService.Application.UseCases.GetPokemonTranslated;

namespace PKMN.PokedexService.Application.Tests.UseCases.GetPokemon;

public class GetPokemonTranslatedQueryValidatorTests
{
    private readonly GetPokemonTranslatedQueryValidator _getPokemonTranslatedQueryValidator;

    private const string NotEmptyValidatorName = "NotEmptyValidator";

    private const string NamePropertyName
        = nameof(GetPokemonTranslatedQuery.Name);

    public GetPokemonTranslatedQueryValidatorTests()
    {
        _getPokemonTranslatedQueryValidator = new GetPokemonTranslatedQueryValidator();
    }

    [Fact]
    public void Should_Validate_True_Because_All_Ok()
    {
        //Arrange
        var query = new GetPokemonTranslatedQuery
        {
            Name = AutoFaker.Generate<string>()
        };

        //Act
        var result = _getPokemonTranslatedQueryValidator.Validate(query);

        //Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Should_Validate_False_Because_Name_Is_Null()
    {
        //Arrange
        var query = new GetPokemonTranslatedQuery
        {
            Name = null!
        };

        //Act
        var result = _getPokemonTranslatedQueryValidator.Validate(query);

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
        var query = new GetPokemonTranslatedQuery
        {
            Name = string.Empty
        };

        //Act
        var result = _getPokemonTranslatedQueryValidator.Validate(query);

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
        var query = new GetPokemonTranslatedQuery
        {
            Name = new string(' ', 5)
        };

        //Act
        var result = _getPokemonTranslatedQueryValidator.Validate(query);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        var first = result.Errors[0];
        first.PropertyName.Should().Be(NamePropertyName);
        first.ErrorCode.Should().Be(NotEmptyValidatorName);
    }
}
