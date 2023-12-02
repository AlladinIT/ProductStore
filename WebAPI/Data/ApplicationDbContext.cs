namespace WebAPI.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Store> Stores { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    
    public void ExecuteSqlFromFile(string filePath)
    {
        var sql = File.ReadAllText(filePath);
        Database.ExecuteSqlRaw(sql);
    }
}