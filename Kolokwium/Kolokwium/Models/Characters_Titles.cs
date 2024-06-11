using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Kolokwium.Models;

[Table("Character_Titles")]
public class Character_Titles
{
    [ForeignKey("Characters")]
    [Column("FK_charact")]
    public int CharacterID { get; set; }
    public Characters Characters { get; set; }

    [ForeignKey("Titles")]
    [Column("FK_title")]
    public int TitleID { get; set; }
    public Titles Titles { get; set; }
    
    [Column("aquire_at")]
    public DateTime aquire_at { get; set; }
}