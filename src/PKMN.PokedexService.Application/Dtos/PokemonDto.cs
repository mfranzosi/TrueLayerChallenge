namespace PKMN.PokedexService.Application.Dtos
{
    public class PokemonDto
    {
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required string? Habitat { get; init; }
        public required bool IsLegendary { get; init; }
    }
}
