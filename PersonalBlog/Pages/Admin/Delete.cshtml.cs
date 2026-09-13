using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly BlogContext _context;
        public DeleteModel(BlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Article requestedArticle = await _context.Articles.FindAsync(id);

            if (requestedArticle is null)
            {
                return NotFound();
            }

            Article = requestedArticle;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Article deletingArticle = await _context.Articles.FindAsync(id);

            if (deletingArticle is not null)
            {
                Article = deletingArticle;
                _context.Articles.Remove(Article);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Admin/Index");
        }
    }
}
