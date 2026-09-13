using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly BlogContext _context;
        public EditModel(BlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Article requestedArticle = await _context.Articles.FindAsync(id);
            if (requestedArticle is null)
            {
                return NotFound();
            }

            Article = requestedArticle;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Article articleToUpdate = await _context.Articles.FindAsync(id);

            if (articleToUpdate is null)
            {
                return NotFound();
            }

            articleToUpdate.Title = Article.Title;
            articleToUpdate.Description = Article.Description;

            if (Image is not null)
            {
                Directory.CreateDirectory("wwwroot/uploads");
                string baseUrl = "wwwroot/uploads";
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                string filePath = Path.Combine(baseUrl, fileName);

                using (FileStream destinationStream = new FileStream(filePath, FileMode.Create))
                {
                    await Image.CopyToAsync(destinationStream);
                }
                articleToUpdate.ImageUrl = "/uploads/" + fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
