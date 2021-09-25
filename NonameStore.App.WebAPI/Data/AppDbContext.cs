using Microsoft.EntityFrameworkCore;
using MyAppBack.Models;
using MyAppBack.Models.OrderAggregate;
using System.Reflection;

namespace MyAppBack.Infrastructure.Database
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
      Database.EnsureCreated();
    }

    public DbSet<ProductRegion> Regions { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Order> Orders { get; set; }





    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

  }

}
