using System.Threading.Tasks;
using MyAppBack.Models;

namespace MyAppBack.Data.Repos.BasketRepository
{
  public interface IBasketRepository
  {
    Task<Basket> GetBasketAsync(string basketId);
    Task<Basket> UpdateBasketAsync(Basket basket);
    Task<bool> DeleteBasketAsync(string basketId);

  }
}