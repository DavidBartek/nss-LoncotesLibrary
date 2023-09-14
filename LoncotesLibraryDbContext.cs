using Microsoft.EntityFrameworkCore;
using LoncotesLibrary.Models;

public class LoncotesLibraryDbContext : DbContext
{
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<MaterialType> MaterialTypes { get; set; }
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
    public LoncotesLibraryDbContext(DbContextOptions<LoncotesLibraryDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(new Genre[]
        {
            new Genre {Id = 1, Name = "Fantasy"},
            new Genre {Id = 2, Name = "History"},
            new Genre {Id = 3, Name = "Theology"},
            new Genre {Id = 4, Name = "Poetry"},
            new Genre {Id = 5, Name = "Period Piece"},
        });

        modelBuilder.Entity<MaterialType>().HasData(new MaterialType[]
        {
            // 3 - book, periodical, cd...
        });

        modelBuilder.Entity<Patron>().HasData(new Patron[]
        {
            // 2
        });

        modelBuilder.Entity<Material>().HasData(new Material[]
        {
            // 10
        });

        modelBuilder.Entity<Checkout>().HasData(new Checkout[]
        {
            // none immeidately needed... is this block needed, then?
        });
    }
}