using library_system.Business;
using Login.Models; // make sure namespaces match
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")
        ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));

// Business objects
builder.Services.AddScoped<AuthenticationBO>();
builder.Services.AddScoped<AdminBO>();
builder.Services.AddScoped<BookBO>();



// Authentication (cookie-based)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Authentications/SignIn";
        options.AccessDeniedPath = "/Authentications/AccessDenied";
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllersWithViews();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Authentication.Any(u => u.Role == "Admin"))
    {
        var admin = new Authentication
        {
            Username = "admin",
            Name = "System",
            Surname = "Administrator",
            Email = "admin@library.com",
            Role = "Admin"
        };

        // ✅ Hash password with the same PasswordHasher
        var hasher = new PasswordHasher<Authentication>();
        admin.Password = hasher.HashPassword(admin, "Admin@123");

        db.Authentication.Add(admin);
        db.SaveChanges();
    }
}

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Session must come before Authentication & Authorization
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentications}/{action=SignIn}/{id?}");

app.Run();
