using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyListingRazorPages.Models;

namespace PropertyListingRazorPages.Data
{
    public class PropertyListingContext : DbContext
    {
        public PropertyListingContext (DbContextOptions<PropertyListingContext> options)
            : base(options)
        {
        }

        public DbSet<PropertyListingRazorPages.Models.Property> Property { get; set; } = default!;
    }
}
