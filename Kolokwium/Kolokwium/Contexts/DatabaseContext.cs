using Kolokwium.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium.Contexts;

public class DatabaseContext: DbContext
{
    public DbSet<Items> Items { get; set; }
    public DbSet<Titles> Titles { get; set; }
    public DbSet<Characters> Characters { get; set; }

    public DbSet<Character_Titles> Character_Titles { get; set; }
    public DbSet<Backpack_Slots> Backpack_Slots { get; set; }

    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character_Titles>()
            .HasKey(ct => new { ct.CharacterID, ct.TitleID });
        
        //klucz po charakterze
        modelBuilder.Entity<Character_Titles>()
            .HasOne(ct => ct.Characters)
            .WithMany(c => c.Character_Titles)
            .HasForeignKey(ct => ct.CharacterID);

        //klucz po tytule
        modelBuilder.Entity<Character_Titles>()
            .HasOne(ct => ct.Titles)
            .WithMany(t => t.Character_Titles)
            .HasForeignKey(pc => pc.TitleID);
        
        modelBuilder.Entity<Backpack_Slots>()
            .HasOne(bc => bc.Items)
            .WithMany(c => c.Backpack_Slots)
            .HasForeignKey(sc => sc.itemID);

        modelBuilder.Entity<Backpack_Slots>()
            .HasOne(o => o.Characters)
            .WithMany(s => s.Backpack_Slots)
            .HasForeignKey(sc => sc.characterID);

    }
}