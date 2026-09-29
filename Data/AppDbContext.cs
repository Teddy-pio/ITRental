using Microsoft.EntityFrameworkCore;
using ITRental.Models;

namespace ITRental.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Equipment>().HasQueryFilter(e => !e.IsDeleted);
    }
}