using Microsoft.EntityFrameworkCore;
using Login.Models;
using library_system.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Authentication> Authentication { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Book> Book{ get; set; }
    public DbSet<Tipology> Tipology { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<Pubblisher> Pubblisher { get; set; }
}
