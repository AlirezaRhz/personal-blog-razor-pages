using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Models;

namespace PersonalBlog.Data
{

    // The other way of ( public class BlogContext(DbContextOptions<BlogContext> options) : DbContext(options) ) is literally the same thing but in a newer and shorter syntax.
    public class BlogContext : IdentityDbContext<ApplicationUser>
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // REQUIRED — sets up Identity's own tables (AspNetUsers, AspNetRoles, ...)
        }
    }
}
