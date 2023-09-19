using LoncotesLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<LoncotesLibraryDbContext>(builder.Configuration["LoncotesLibraryDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// get list of all circulating materials
// include genre, materialtype
// exclude materials with an OutOfCirculation value
// reservations list will be "null" for this endpoint (only populates when accessing one material by id)

// IN ADDITION:
// should be able to get a list of materials filtered by genre(id) and/or materialtype(id)

// ERROR HANDLING:
// must verify that genreId and materialTypeId, if passed in, exist

app.MapGet("/api/materials", (LoncotesLibraryDbContext db, int? genreId, int? materialTypeId) =>
{
    var query = db.Materials
        .Include(m => m.Genre)
        .Include(m => m.MaterialType)
        .Where(m => m.OutOfCirculationSince == null);
    
    if (genreId.HasValue)
    {
        query = query.Where(m => m.GenreId == genreId);
    }

    if (materialTypeId.HasValue)
    {
        query = query.Where(m => m.MaterialTypeId == materialTypeId);
    }

    var results = query.ToList();

    return results;
});

// get material by specific id
// include genre, materialtype, checkouts, and linked patron for each checkout.
app.MapGet("/api/materials/{id}", (LoncotesLibraryDbContext db, int id) =>
{
    try
    {
        Material foundMaterial = db.Materials
            .Include(m => m.Genre)
            .Include(m => m.MaterialType)
            .Include(m => m.Checkouts)
            .ThenInclude(c => c.Patron)
            .Single(m => m.Id == id);
        return Results.Ok(foundMaterial);
    }
    catch (InvalidOperationException)
    {
        return Results.NotFound();
    }
});

// create new material
app.MapPost("/api/materials", (LoncotesLibraryDbContext db, Material newMaterial) =>
{
    db.Materials.Add(newMaterial);
    db.SaveChanges();
    return Results.Created($"/api/materials/{newMaterial.Id}", newMaterial);
});

// "soft delete" a material by id (mark OutOfCirculationSince as DateTime.Now).
// note, this could also be handled as a PUT, since it's technically just modifying a prop.
// but in this case, no payload (a Material object) is necessary to pass in. Therefore, DELETE is sufficient.
app.MapDelete("/api/materials/{id}", (LoncotesLibraryDbContext db, int id) =>
{
    Material foundMaterial = db.Materials.SingleOrDefault(m => m.Id == id);
    if (foundMaterial == null)
    {
        return Results.NotFound();
    }
    
    foundMaterial.OutOfCirculationSince = DateTime.Now;
    db.SaveChanges();
    return Results.NoContent();
});

// get list of all available materials (has a related checkout with a return date != null)
app.MapGet("/api/materials/available", (LoncotesLibraryDbContext db) =>
{
    return db.Materials
        .Where(m => m.OutOfCirculationSince == null)
        .Where(m => m.Checkouts.All(co => co.ReturnDate != null))
        .ToList();
});

// get list of all overdue checkouts (where Today - checkout date is > material type's days checkout days)
app.MapGet("/api/checkouts/overdue", (LoncotesLibraryDbContext db) =>
{
    return db.Checkouts
        .Include(p => p.Patron)
        .Include(co => co.Material)
        .ThenInclude(m => m.MaterialType)
        .Where(co =>
            co.ReturnDate == null &&
            (DateTime.Today - co.CheckoutDate).Days >
            co.Material.MaterialType.CheckoutDays
            )
        .ToList();
});

// get list of all material types
app.MapGet("/api/materialtypes", (LoncotesLibraryDbContext db) =>
{
    return db.MaterialTypes.ToList();
});

// get list of all genre types
app.MapGet("/api/genres", (LoncotesLibraryDbContext db) =>
{
    return db.Genres.ToList();
});

// get list of all patrons (not expanded)
app.MapGet("/api/patrons", (LoncotesLibraryDbContext db) =>
{
    return db.Patrons.ToList();
});

// get patron by id
// include reservations associated, and material assoc with each reservation, and material type associated with each material
// NEEDS TO BE CHECKED ONCE RESERVATIONS EXIST
app.MapGet("/api/patrons/{id}", (LoncotesLibraryDbContext db, int id) =>
{
    try
    {
        Patron foundPatron = db.Patrons
            .Include(p => p.Checkouts)
                .ThenInclude(c => c.Material)
                    .ThenInclude(m => m.MaterialType)
            .Single(p => p.Id == id);
        return Results.Ok(foundPatron);
    }
    catch (InvalidOperationException)
    {
        return Results.NotFound();
    }
});

// edit patron's address and/or email address (not name)

app.MapPut("/api/patrons/{id}", (LoncotesLibraryDbContext db, int id, Patron modifiedPatron) =>
{
    Patron foundPatron = db.Patrons.SingleOrDefault(p => p.Id == id);
    if (foundPatron == null)
    {
        return Results.NotFound();
    }
    else if (modifiedPatron.Id != id)
    {
        return Results.BadRequest();
    }
    foundPatron.Address = modifiedPatron.Address;
    foundPatron.Email = modifiedPatron.Email;

    db.SaveChanges();
    return Results.NoContent();

});

// soft delete - deactivate patron (set IsActive to false)

app.MapDelete("/api/patrons/{id}", (LoncotesLibraryDbContext db, int id) =>
{
    Patron foundPatron = db.Patrons.SingleOrDefault(p => p.Id == id);
    if (foundPatron == null)
    {
        return Results.NotFound();
    }

    foundPatron.IsActive = false;
    db.SaveChanges();
    return Results.NoContent();
});

// I MADE THIS:
// GET all checkouts

app.MapGet("/api/checkouts", (LoncotesLibraryDbContext db) =>
{
    return db.Checkouts
        .Include(c => c.Material)
            .ThenInclude(m => m.MaterialType)
        .Include(c => c.Patron)
        .ToList();
});

// create new checkout for a material and patron
// set checkout date to DateTime.Today
// ERROR HANDLING: ensure newCheckout object's materialId and patronId exist
// ERROR HANDLING: ensure materialId is not a material already checked out
// ERROR HANDLING: ensure materialId is not a material out of circulation
// ERROR HANDLING: ensure patronId is not an inactive patron

app.MapPost("/api/checkouts", (LoncotesLibraryDbContext db, Checkout newCheckout) =>
{
    newCheckout.CheckoutDate = DateTime.Today;
    db.Checkouts.Add(newCheckout);
    db.SaveChanges();
    return Results.Created($"/api/checkouts/{newCheckout.Id}", newCheckout);
});

// mark checked out item as returned by item id
// endpoint: expects checkout id
// update checkout with return date of DateTime.Today

app.MapDelete("/api/checkouts/{id}", (LoncotesLibraryDbContext db, int id) =>
{
    Checkout foundCheckout = db.Checkouts.SingleOrDefault(c => c.Id == id);
    if (foundCheckout == null)
    {
        return Results.NotFound();
    }
    foundCheckout.ReturnDate = DateTime.Today;
    db.SaveChanges();
    return Results.NoContent();
});

app.Run();