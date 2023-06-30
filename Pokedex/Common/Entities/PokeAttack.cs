
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Common;

[Table("pokeattack")]
public class PokeAttack
{
    [Key]
    [Column("pokeattackId")]
    public int PokeAttackId { get; set; }

    [Column("fpokemonId")]
    public int FPokemonId { get; set; }

    [Column("fattackId")]
    public int FAttackId { get; set; }

    public static PokeAttack CreateRelation(Pokemon pokemon, Attack attack) => new PokeAttack() {
        FPokemonId = pokemon.PokemonId,
        FAttackId = attack.AttackId
    };
}