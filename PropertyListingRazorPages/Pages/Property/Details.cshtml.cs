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
    public class DetailsModel : PageModel
    {
        private readonly PropertyListingRazorPages.Data.PropertyListingContext _context;

        public DetailsModel(PropertyListingRazorPages.Data.PropertyListingContext context)
        {
            _context = context;
        }

        public Models.Property Property { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var property = await _context.Property.FirstOrDefaultAsync(m => m.Id == id);

            if (property is not null)
            {
                Property = property;

                return Page();
            }

            return NotFound();
        }
    }
}
