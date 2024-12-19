namespace PKMN.PokedexService.Domain.Entities
{
    public class Pokemon
    {
        public required string Name { get; init; }
        public required string Description { get; set; }
        public required string? Habitat { get; init; }
        public required bool IsLegendary { get; init; }
    }
}
