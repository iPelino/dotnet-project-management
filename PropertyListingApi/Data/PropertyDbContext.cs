using Microsoft.EntityFrameworkCore;
using PropertyListingApi.Models;

namespace PropertyListingApi.Data;

public class PropertyDbContext: DbContext
{
    public PropertyDbContext(DbContextOptions<PropertyDbContext> options) : base(options)
    {
    }
    public DbSet<Property> Properties { get; set; }

}