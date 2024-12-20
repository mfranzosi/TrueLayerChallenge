using FluentValidation;

namespace PKMN.PokedexService.Application.UseCases.GetPokemon;

public class GetPokemonQueryValidator : AbstractValidator<GetPokemonQuery>
{
    public GetPokemonQueryValidator()
    {
        RuleFor(query => query.Name).NotEmpty();
    }
}
