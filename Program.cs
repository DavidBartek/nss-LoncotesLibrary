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

app.MapGet("/api/materials", (LoncotesLibraryDbContext db) =>
{
    return db.Materials.Where(m => m.OutOfCirculationSince == null)
        .Include(m => m.Genre)
        .Include(m => m.MaterialType)
        .ToList();
});

// // get list of materials by genre and/or materialtype
// how do I set up a route to handle all these different conditions
// ^ sounds like this will be set up in this same endpoint

// app.MapGet("/api/materials/")



// get material by specific id
// include genre, materialtype, checkouts, and linked patron for each checkout.
// NEEDS TO BE CHECKED ONCE RESERVATIONS EXIST
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

// create new checkout for a material and patron
// set checkout date to DateTime.Today

// mark checked out item as returned by item id
// endpoint: expects checkout id
// update checkout with return date of DateTime.Today

app.Run();