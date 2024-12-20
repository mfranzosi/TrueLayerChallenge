using MediatR;
using Microsoft.AspNetCore.Mvc;
using PKMN.PokedexService.Application.Dtos;
using PKMN.PokedexService.Application.UseCases.GetPokemon;
using PKMN.PokedexService.Application.UseCases.GetPokemonTranslated;

namespace PKMN.PokedexService.Api.Controllers
{
    [ApiController]
    public class PokemonController(ILogger<PokemonController> logger, IMediator mediator) : ControllerBase
    {

        /// <summary>
        /// Gets information about a Pokemon by its name.
        /// </summary>
        /// <param name="name">The name of the Pokemon.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>The information about the Pokemon.</returns>
        [HttpGet("pokemon/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PokemonDto>> GetPokemon(
      string name,
      CancellationToken cancellationToken = default)
        {
            var query = new GetPokemonQuery
            {
                Name = name
            };

            var result = await mediator.Send(query, cancellationToken);

            if (result is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        /// <summary>
        /// Gets information about a Pokemon by its name, including a funny translated description.
        /// </summary>
        /// <param name="name">The name of the Pokemon.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>The information about the Pokemon.</returns>
        [HttpGet("pokemon/translated/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PokemonDto>> GetPokemonTranslated(
      string name,
      CancellationToken cancellationToken = default)
        {
            var query = new GetPokemonTranslatedQuery
            {
                Name = name
            };

            var result = await mediator.Send(query, cancellationToken);

            if (result is not null)
            {

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
