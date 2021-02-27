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
    public class IndexModel : PageModel
    {
        private readonly EfAndDb.WebRazorApp.Data.ApplicationDbContext _context;

        public IndexModel(EfAndDb.WebRazorApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Blog> Blog { get;set; }

        public async Task OnGetAsync()
        {
            Blog = await _context.Blogs.ToListAsync();
        }
    }
}
