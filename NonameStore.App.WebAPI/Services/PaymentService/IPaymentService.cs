using System.Threading.Tasks;
using NonameStore.App.WebAPI.Models;
using NonameStore.App.WebAPI.Models.OrderAggregate;

namespace NonameStore.App.WebAPI.Services.PaymentService
{
  public interface IPaymentService
  {
    Task<Basket> CreateOrUpdatePaymentIntent(string basketId);
    Task<Order> UpdateOrderPaymentSucceded(string paymentInentId);
    Task<Order> UpdateOrderPaymentFailed(string paymentInentId);
    Task<Basket> CreateOrUpdateOffline(string basketId);


  }
}