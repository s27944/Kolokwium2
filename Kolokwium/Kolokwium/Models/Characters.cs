namespace Kolokwium.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("Characters")]
public class Characters
{
    [Key]
    [Column("PK")]
    public int PK { get; set; }
    
    [Column("first_name")]
    [MaxLength(50)]
    public string firstName { get; set; }
    
    [Column("last_name")]
    [MaxLength(50)]
    public string last_name { get; set; }
    
    [Column("current_weig")]
    public int current_weig { get; set; }
    
    [Column("max_weight")]
    public int max_weight { get; set; }
    
    [Column("money")]
    public int money { get; set; }
    

    public IEnumerable<Character_Titles> Character_Titles;
    public IEnumerable<Backpack_Slots> Backpack_Slots;
}