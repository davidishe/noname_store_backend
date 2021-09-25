using System;
using System.Collections.Generic;

namespace NonameStore.App.WebAPI.Models
{
  public class Basket
  {
    public Basket(string id)
    {
      Id = id;
      Items = new List<BasketItem>();
    }

    public Basket()
    {
    }

    public string Id { get; set; }
    public List<BasketItem> Items { get; set; }
    public int? DeliveryMethodId { get; set; }
    public string? ClientSecret { get; set; }
    public string? PaymentIntentId { get; set; }
    public string? ConfirmationUrl { get; set; }
    public int? ShippingPrice { get; set; }
    public string? OrderNumber { get; set; }

  }

}