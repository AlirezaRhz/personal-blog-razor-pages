using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly BlogContext _context;
        public CreateModel(BlogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Article Article { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

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
                Article.ImageUrl = "/uploads/" + fileName;
            }
            else
            {
                Random rand = new Random();
                Article.ImageUrl = $"/images/Default-Article-Image-{rand.Next(1, 3)}.jpg";
            }

            Article.CreatedAt = DateTime.UtcNow;

            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
