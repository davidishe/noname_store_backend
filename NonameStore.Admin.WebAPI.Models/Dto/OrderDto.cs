using NonameStore.Admin.WebAPI.Models.Models;

namespace NonameStore.Admin.WebAPI.Models.Dtos
{
  public class OrderDto
  {
    public string BasketId { get; set; }
    public int DeliveryMethodId { get; set; }
    public AddressDto ShipToAddress { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
  }
}