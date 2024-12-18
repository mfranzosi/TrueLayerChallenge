namespace PKMN.PokedexService.Domain.Entities
{
    public class Pokemon
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string? Habitat { get; set; }
        public required bool IsLegendary { get; set; }
    }
}
