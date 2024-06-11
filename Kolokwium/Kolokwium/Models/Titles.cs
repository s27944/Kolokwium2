using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Kolokwium.Models;

[Table("Titles")]
public class Titles
{
    [Key]
    [Column("PK")]
    public int PK { get; set; }
    
    [Column("nam")]
    [MaxLength(100)]
    public string name { get; set; }

    public IEnumerable<Character_Titles> Character_Titles;
}