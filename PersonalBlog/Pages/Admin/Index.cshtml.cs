using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly BlogContext _context;
        public IndexModel(BlogContext context)
        {
            _context = context;
        }

        public IList<Article> Articles { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Articles = await _context.Articles.ToListAsync();
        }
    }
}
