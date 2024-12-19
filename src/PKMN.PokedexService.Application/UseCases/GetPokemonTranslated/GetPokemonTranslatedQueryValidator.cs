using FluentValidation;

namespace PKMN.PokedexService.Application.UseCases.GetPokemonTranslated;

public class GetPokemonTranslatedQueryValidator : AbstractValidator<
    GetPokemonTranslatedQuery>
{
    public GetPokemonTranslatedQueryValidator()
    {
        RuleFor(query => query.Name).NotEmpty();
    }
}
