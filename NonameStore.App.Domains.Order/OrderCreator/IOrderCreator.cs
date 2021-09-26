using System.Threading.Tasks;
using NonameStore.App.WebAPI.Models.OrderAggregate;

namespace NonameStore.App.Domains.OrderCreator
{
  public interface IOrderCreator
  {
    Task<bool> CreateOrderInDatabase(Order order);
  }
}