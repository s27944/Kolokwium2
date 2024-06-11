using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium.Models;

[Table("Items")]
public class Items
{
    [Key]
    [Column("PK")]
    public int PK { get; set; }
    
    [Column("name")]
    [MaxLength(50)]
    public string name { get; set; }
    
    [Column("weig")]
    public int weig { get; set; }
    
    public IEnumerable<Backpack_Slots> Backpack_Slots;
}