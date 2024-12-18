using MediatR;
using Microsoft.AspNetCore.Mvc;
using PKMN.PokedexService.Application.Dtos;
using PKMN.PokedexService.Application.UseCases.GetPokemon;

namespace PKMN.PokedexService.Api.Controllers
{
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IMediator _mediator;

        public PokemonController(ILogger<PokemonController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

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

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
