using Food.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Food.DataAccess;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    public DbSet<Category> Categories { get; set; }
    public DbSet<LevelType> LevelType { get; set; }
    public DbSet<Dish> Dish { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
}

