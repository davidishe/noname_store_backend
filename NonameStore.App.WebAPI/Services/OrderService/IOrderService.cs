using System.Collections.Generic;
using System.Threading.Tasks;
using MyAppBack.Models.OrderAggregate;

namespace MyAppBack.Services.OrderService
{
  public interface IOrderService
  {
    Task<Order> CreateOrderAsync(string byerEmail, int deliveryMethodId, string basketId, Address shipingAddress, PaymentMethod PaymentMethod);
    Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string byerEmail);
    Task<Order> GetOrderById(int id, string byerEmail);
    Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    Task<Order> GetOrderByPaymentIntent(string paymentIntentId);

    Task<Order> UpdateOrderAsync(int orderId, int deliveryMethodId);





  }
}