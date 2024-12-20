using AutoBogus;
using FluentAssertions;
using PKMN.PokedexService.Application.Options;

namespace PKMN.PokedexService.Application.Tests.Options;

public class TranslationApiOptionsValidatorTests
{
    [Fact]
    public void Validates_True()
    {
        // Arrange
        var validator = new TranslationApiOptionsValidator();

        // Act
        var result = validator.Validate(new TranslationApiOptions
        {
            YodaTranslationApiURI = AutoFaker.Generate<string>(),
            ShakespeareTranslationApiURI = AutoFaker.Generate<string>(),
        });

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("uri", null)]
    [InlineData("uri", "")]
    [InlineData("uri", " ")]
    [InlineData(null, "uri")]
    [InlineData("", "uri")]
    [InlineData(" ", "uri")]
    public void Validates_False_Because_Values_Are_Null_Empty_Or_Whitespace(string? yodaUri, string? shakespeareUri)
    {
        // Arrange
        var validator = new TranslationApiOptionsValidator();

        // Act
        var result = validator.Validate(new TranslationApiOptions
        {
            YodaTranslationApiURI = yodaUri!,
            ShakespeareTranslationApiURI = shakespeareUri!
        });

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
