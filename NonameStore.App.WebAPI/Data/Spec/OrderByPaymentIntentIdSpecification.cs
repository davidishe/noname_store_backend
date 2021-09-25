using System;
using MyAppBack.Helpers;
using MyAppBack.Models;
using MyAppBack.Models.OrderAggregate;

namespace MyAppBack.Data.Spec
{
  public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
  {
    public OrderByPaymentIntentIdSpecification(UserParams userParams)
    : base(x =>
          (string.IsNullOrEmpty(userParams.Search) || x.ByerEmail.ToLower().Contains(userParams.Search.ToLower())) &&
          (userParams.IsAdmin == true)
        )
    {
      ApplyPaging((userParams.PageSize * (userParams.PageIndex)), userParams.PageSize);

      if (!string.IsNullOrEmpty(userParams.sort))
      {
        switch (userParams.sort)
        {
          case "priceAsc":
            AddOrderByAscending(p => p.ByerEmail);
            break;
          case "priceDesc":
            AddOrderByDescending(s => s.ByerEmail);
            break;
          case "name":
            AddOrderByAscending(s => s.ByerEmail);
            break;
          default:
            AddOrderByDescending(x => x.OrderDate);
            break;
        }
      }
    }

    public OrderByPaymentIntentIdSpecification(string paymetnId) : base(x => x.PaymentIntentId == paymetnId)
    {
      AddInclude(x => x.ShipToAddress);
    }
  }


}