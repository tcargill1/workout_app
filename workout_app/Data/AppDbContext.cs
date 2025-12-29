using Microsoft.EntityFrameworkCore;
using workout_app.Models;

namespace workout_app.Data;

public class AppDbContext : DbContext 
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
}