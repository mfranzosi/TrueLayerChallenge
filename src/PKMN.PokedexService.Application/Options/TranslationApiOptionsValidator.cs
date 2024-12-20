using FluentValidation;

namespace PKMN.PokedexService.Application.Options;

public class TranslationApiOptionsValidator : AbstractValidator<TranslationApiOptions>
{
    public TranslationApiOptionsValidator()
    {
        RuleFor(uri => uri.YodaTranslationApiURI).NotEmpty();        

        RuleFor(uri => uri.ShakespeareTranslationApiURI).NotEmpty();        
    }
}
