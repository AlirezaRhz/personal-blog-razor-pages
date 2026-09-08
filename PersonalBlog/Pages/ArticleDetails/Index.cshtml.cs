using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Pages.ArticleDetails
{
    public class DetailsModel : PageModel
    {
        private readonly BlogContext _context;
        public DetailsModel(BlogContext context)
        {
            _context = context;
        }

        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Article? article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            Article = article;
            return Page();
        }
    }
}
