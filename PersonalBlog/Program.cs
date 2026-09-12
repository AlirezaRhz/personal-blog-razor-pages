using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// We are setting the policy "AdminOnly" for the Razor Pages authorization.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin", "AdminOnly"); // Apply the "AdminOnly" policy to the "/Admin" folder
});

// Add Authorization to be able to configure it ourselves :
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

// Register the BlogContext with the dependency injection container
builder.Services.AddDbContext<BlogContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("BlogContext"));
});


// Add ASP.NET Identity services to the container
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Require email confirmation for sign-in
}).AddRoles<IdentityRole>() // Add role management
  .AddEntityFrameworkStores<BlogContext>(); // Use BlogContext for Identity

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    List<string> roles = new List<string> { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
