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
            new Genre {Id = 6, Name = "Fiction"},
            new Genre {Id = 7, Name = "Pop Punk"},
            new Genre {Id = 8, Name = "Horror"},
            new Genre {Id = 9, Name = "Textbook"}
        });

        modelBuilder.Entity<MaterialType>().HasData(new MaterialType[]
        {
            new MaterialType {Id = 1, Name = "Book", CheckoutDays = 14},
            new MaterialType {Id = 2, Name = "CD", CheckoutDays = 7},
            new MaterialType {Id = 3, Name = "Academic Journal", CheckoutDays = 14},
            new MaterialType {Id = 4, Name = "DVD", CheckoutDays = 7}
        });

        modelBuilder.Entity<Patron>().HasData(new Patron[]
        {
            // 2
            new Patron {Id = 1, FirstName = "John", LastName = "McJohn", Address = "123 Street Rd", Email = "john@john.com", IsActive = true},
            new Patron {Id = 2, FirstName = "Mark", LastName = "McMark", Address = "123 Road St", Email = "mark@mark.com", IsActive = true},
            new Patron {Id = 3, FirstName = "Jake", LastName = "McJake", Address = "321 Road St", Email = "jake@jake.com", IsActive = false}
        });

        modelBuilder.Entity<Material>().HasData(new Material[]
        {
            // 10
            new Material {Id = 1, MaterialName = "Harry Potter and the Sorcerer's Stone", MaterialTypeId = 1, GenreId = 6},
            new Material {Id = 2, MaterialName = "Harry Potter and the Chamber of Secrets", MaterialTypeId = 1, GenreId = 6},
            new Material {Id = 3, MaterialName = "Harry Potter and the Prisoner of Azkaban", MaterialTypeId = 1, GenreId = 6},
            new Material {Id = 4, MaterialName = "Harry Potter and the Goblet of Fire", MaterialTypeId = 1, GenreId = 6},
            new Material {Id = 5, MaterialName = "Harry Potter and the Half-Blood Prince", MaterialTypeId = 1, GenreId = 6},
            new Material {Id = 6, MaterialName = "Presbyterion, vol. 1, Spring 1986", MaterialTypeId = 3, GenreId = 3, OutOfCirculationSince = new DateTime(1986, 12, 31)},
            new Material {Id = 7, MaterialName = "Commit This To Memory", MaterialTypeId = 2, GenreId = 7, OutOfCirculationSince = new DateTime(2006, 11, 30)},
            new Material {Id = 8, MaterialName = "Hirohito's War", MaterialTypeId = 1, GenreId = 2},
            new Material {Id = 9, MaterialName = "Peewee's Big Adventure", MaterialTypeId = 4, GenreId = 8, OutOfCirculationSince = new DateTime(2000, 9, 1)},
            new Material {Id = 10, MaterialName = "Operating System Concepts, 8th ed", MaterialTypeId = 1, GenreId = 9, OutOfCirculationSince = new DateTime(2008, 11, 30)}
        });

        modelBuilder.Entity<Checkout>().HasData(new Checkout[]
        {
            // none needed for seed
        });
    }
}