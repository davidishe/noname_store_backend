using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NonameStore.Admin.WebAPI.Models.Models;

namespace NonameStore.Admin.Database
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options)
      : base(options)
    {
      Database.EnsureCreated();
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
    }

  }

}
