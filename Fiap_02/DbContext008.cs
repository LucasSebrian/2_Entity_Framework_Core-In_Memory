using Microsoft.EntityFrameworkCore;

namespace Database008;

public class DbContext008 : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Database008");
    }
}
