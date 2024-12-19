using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PropertyListingApi.Data;
using PropertyListingApi.Models;
using PropertyListingRazorPages.Data;

var builder = WebApplication.CreateBuilder(args);


const string connString = "Data Source=property.db";

// builder.Services.AddSqlite<PropertyDbContext>(connString);
builder.Services.AddDbContext<PropertyDbContext>(options => options.UseSqlite(connString));
var app = builder.Build();


// IN MEMORY DATABASE
// ========================================
// List<Property> properties = [
//     new Property { Id = 1, Name = "Property 1", Address = "Address 1" },
//     new Property { Id = 2, Name = "Property 2", Address = "Address 2" },
//     new Property { Id = 3, Name = "Property 3", Address = "Address 3" }
// ];
//
// // get all properties
// app.MapGet("properties", () => properties);


// // get a property by id
// app.MapGet("/properties/{id}", (int id) =>
// {
//     var property = properties.FirstOrDefault(p => p.Id == id);
//     // if (property is null)
//     // {
//     //     return Results.NotFound("Property not found");
//     // }
//     // return Results.Ok(property);
//
//
//     return property is not null ? Results.Ok(property) : Results.NotFound();
//
// });
//
// // post a property
// app.MapPost("/properties", (Property newProperty) => {
//     properties.Add(newProperty);
//     return newProperty;
// });
//
// // update a property
// app.MapPut("/properties/{id}", (int id, Property updatedProperty) => {
//     var property = properties.FirstOrDefault(p => p.Id == id);
//     if (property is null)
//     {
//         return Results.NotFound();
//     }
//
//     var index = properties.FindIndex(p => p.Id == id);
//     properties[index] = updatedProperty;
//
//     return Results.NoContent();
// });
//
// // delete a property
// app.MapDelete("/properties/{id}", (int id) => {
//     var property = properties.FirstOrDefault(p => p.Id == id);
//     if (property is null)
//     {
//         return Results.NotFound();
//     }
//
//     properties.Remove(property);
//     return Results.NoContent();
// });

// SQLITE DATABASE
// ========================================
app.MapGet("/properties", async (PropertyDbContext dbContext) =>
{
    var properties = await dbContext.Properties.ToListAsync();
    return properties;
});

app.MapGet("/properties/{id}", async (int id, PropertyDbContext dbContext) =>
{
    var property = await dbContext.Properties.FindAsync(id);
    return property is not null ? Results.Ok(property) : Results.NotFound();
});

app.MapPost("/properties", async (Property property, PropertyDbContext dbContext) =>
{
    try
    {
        dbContext.Properties.Add(property);
        await dbContext.SaveChangesAsync();
        return Results.Created($"/properties/{property.Id}", property);
    }
    catch (DbUpdateException e) when (e.InnerException is SqliteException sqliteEx && sqliteEx.SqliteErrorCode == 19)
    {
        return Results.Conflict("A property with the same ID already exists.");
    }
    catch (Exception e)
    {
        return Results.Problem(statusCode:500, detail:e.Message, title:"Error");
    }

});

app.MapPut("/properties/{id}", async (int id, Property updatedProperty, PropertyDbContext dbContext) =>
{
    var property = await dbContext.Properties.FindAsync(id);
    if (property is null)
    {
        return Results.NotFound();
    }

    property.Name = updatedProperty.Name;
    property.Address = updatedProperty.Address;

    await dbContext.SaveChangesAsync();
    return Results.NoContent();
});


app.MapDelete("/properties/{id}", async (int id, PropertyDbContext dbContext) =>
{
    var property = await dbContext.Properties.FindAsync(id);
    if (property is null)
    {
        return Results.NotFound();
    }

    dbContext.Properties.Remove(property);
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();