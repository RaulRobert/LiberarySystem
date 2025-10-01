using LiberarySystem.Models;
using library_system.Business;
using library_system.Middleware;

//using library_system.Middleware;
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
builder.Services.AddScoped<AuthorBO>();
builder.Services.AddScoped<PubblisherBO>();
builder.Services.AddScoped<TipologyBO>();



// Authentication (cookie-based)
builder.Services.AddDistributedMemoryCache();
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

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRouting();

// ✅ Session must come before auth
app.UseSession();

// ✅ Custom middleware for token validation

app.UseMiddleware<SessionValidationMiddleware>();

app.UseAuthentication();
app.UseAuthorization();



// First, allow area routes
app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Books}/{action=Index}/{id?}");

// Fallback for non-area controllers like Authentications
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentications}/{action=SignIn}/{id?}");


app.Run();
