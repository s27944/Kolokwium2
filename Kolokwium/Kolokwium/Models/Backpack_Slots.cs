using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Kolokwium.Models;


[Table("Backpack_Slots")]
public class Backpack_Slots
{
    [Key]
    [Column("PK")]
    public int PK { get; set; }
    
    [ForeignKey("Items")]
    [Column("FK_item")]
    public int itemID { get; set; }
    public Items Items { get; set; }

    
    [ForeignKey("Characters")]
    [Column("FK_character")]
    public int characterID { get; set; }
    public Characters Characters { get; set; }
}