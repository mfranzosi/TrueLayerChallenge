namespace PKMN.PokedexService.Domain.Entities;

/// <summary>
/// This class contains information about a Pokémon species.
/// </summary>
public class Pokemon
{
    /// <summary>
    /// The name of the Pokémon species.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// A brief description of the main characteristics and behavior of the Pokémon species.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Habitat is the place where a Pokémon species originally lives. It is a feature included only in the Pokédex of Pokémon Fire Red and Leaf Green.
    /// <para>Habitat can be null for some Pokémon species, for example Solgaleo (It is said that he lives in another world).</para>
    /// </summary>
    public required string? Habitat { get; init; }

    /// <summary>
    /// A boolean flag indicating whether the Pokémon is Legendary or not. 
    /// <para>A Legendary Pokémon is a unique type of Pokémon that is renowned for its exceptional power and rarity.</para>
    /// </summary>
    public required bool IsLegendary { get; init; }
}
