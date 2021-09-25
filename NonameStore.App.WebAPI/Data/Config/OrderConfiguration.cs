using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NonameStore.App.WebAPI.Models.OrderAggregate;

namespace NonameStore.App.WebAPI.Data.Config
{
  public class OrderConfiguration : IEntityTypeConfiguration<Order>
  {

    public void Configure(EntityTypeBuilder<Order> builder)
    {
      builder.OwnsOne(o => o.ShipToAddress, a => { a.WithOwner(); });
      builder.Property(s => s.Status)
        .HasConversion(
            o => o.ToString(),
            o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
        );

      builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
    }
  }
}