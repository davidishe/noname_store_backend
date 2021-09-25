using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NonameStore.App.WebAPI.Dtos
{
  public class BasketDto
  {
    [Required]
    public string Id { get; set; }
    public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    public int? DeliveryMethodId { get; set; }
    public string? ClientSecret { get; set; }
    public string? PaymentIntentId { get; set; }
    public int? ShippingPrice { get; set; }
    public string? OrderNumber { get; set; }

  }
}