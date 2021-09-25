using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NonameStore.App.WebAPI.Dtos
{
  public class BasketDto
  {
    public string Id { get; set; }
    public List<BasketItemDto> Items { get; set; }
    public int? DeliveryMethodId { get; set; }
    public string? ClientSecret { get; set; }
    public string? PaymentIntentId { get; set; }
    public int? ShippingPrice { get; set; }
    public string? OrderNumber { get; set; }

  }
}