using System.Threading.Tasks;
using NonameStore.App.WebAPI.Models.Dtos;

namespace NonameStore.App.Domains.Order.OrderCreator
{
  public interface IOrderCreator
  {
    Task<bool> CreateOrderInDatabase(OrderDto orderDto);
  }
}