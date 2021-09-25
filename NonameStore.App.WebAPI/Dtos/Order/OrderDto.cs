
using MyAppBack.Models.OrderAggregate;

namespace MyAppBack.Dtos
{
  public class OrderDto
  {
    public string BasketId { get; set; }
    public int DeliveryMethodId { get; set; }
    public AddressDto ShipToAddress { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

  }
}