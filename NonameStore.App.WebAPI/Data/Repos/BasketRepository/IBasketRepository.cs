using System.Threading.Tasks;
using NonameStore.App.WebAPI.Models;

namespace NonameStore.App.WebAPI.Data.Repos.BasketRepository
{
  public interface IBasketRepository
  {
    Task<Basket> GetBasketAsync(string basketId);
    Task<Basket> UpdateBasketAsync(Basket basket);
    Task<bool> DeleteBasketAsync(string basketId);

  }
}