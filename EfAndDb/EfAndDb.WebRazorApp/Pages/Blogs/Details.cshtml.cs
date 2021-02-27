using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EfAndDb.WebRazorApp.Data;
using EfAndDb.WebRazorApp.Models;

namespace EfAndDb.WebRazorApp.Pages.Blogs
{
    public class DetailsModel : PageModel
    {
        private readonly EfAndDb.WebRazorApp.Data.ApplicationDbContext _context;

        public DetailsModel(EfAndDb.WebRazorApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Blog Blog { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Blog = await _context.Blogs.FirstOrDefaultAsync(m => m.BlogId == id);

            if (Blog == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
