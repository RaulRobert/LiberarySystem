using LiberarySystem.Models;
using library_system.Models;
using Login.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



        base.OnModelCreating(modelBuilder);

        // Seeding Roles
        modelBuilder.Entity<Role>().HasData(
        new Role { Id = 1, Name = "Admin" },
        new Role { Id = 2, Name = "Employee" },
        new Role { Id = 3, Name = "User" }


            );
    }

    public DbSet<Authentication> Authentication { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Book> Book{ get; set; }
    public DbSet<Borrow> Borrows { get; set; }

    public DbSet<Tipology> Tipology { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<Pubblisher> Pubblisher { get; set; }
    public DbSet<Role> Role { get; set; }

}
