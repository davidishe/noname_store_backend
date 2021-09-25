
using NonameStore.App.WebAPI.Dtos;
using NonameStore.App.WebAPI.Models.OrderAggregate;

namespace NonameStore.App.WebAPI.Models.Dtos
{
  public class OrderDto
  {
    public string BasketId { get; set; }
    public int DeliveryMethodId { get; set; }
    public AddressDto ShipToAddress { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

  }
}