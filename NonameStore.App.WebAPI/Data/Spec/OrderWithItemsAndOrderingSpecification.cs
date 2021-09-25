using System;
using System.Linq.Expressions;
using MyAppBack.Models.OrderAggregate;

namespace MyAppBack.Data.Spec
{
  public class OrderWithItemsAndOrderingSpecification : BaseSpecification<Order>
  {
    public OrderWithItemsAndOrderingSpecification(string email) : base(o => o.ByerEmail == email)
    {
      AddInclude(o => o.OrderItems);
      AddInclude(o => o.DeliveryMethod);
      AddOrderByDescending(o => o.OrderDate);
    }

    public OrderWithItemsAndOrderingSpecification(int id, string email) : base(o => o.Id == id && o.ByerEmail == email)
    {
      AddInclude(o => o.OrderItems);
      AddInclude(o => o.DeliveryMethod);
    }
  }
}