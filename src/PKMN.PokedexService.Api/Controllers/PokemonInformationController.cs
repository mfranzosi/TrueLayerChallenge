using Microsoft.AspNetCore.Mvc;

namespace PKMN.PokedexService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonInformationController : ControllerBase
    {
        private readonly ILogger<PokemonInformationController> _logger;

        public PokemonInformationController(ILogger<PokemonInformationController> logger)
        {
            _logger = logger;
        }
    }
}
