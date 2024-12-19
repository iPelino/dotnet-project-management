using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PropertyListingRazorPages.Data;
using PropertyListingRazorPages.Models;

namespace PropertyListingRazorPages.Pages.Property
{
    public class IndexModel : PageModel
    {
        private readonly PropertyListingRazorPages.Data.PropertyListingContext _context;

        public IndexModel(PropertyListingRazorPages.Data.PropertyListingContext context)
        {
            _context = context;
        }

        public IList<Models.Property> Property { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Property = await _context.Property.ToListAsync();
        }
    }
}
