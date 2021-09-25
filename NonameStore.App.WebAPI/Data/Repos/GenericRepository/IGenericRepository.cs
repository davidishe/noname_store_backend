using System.Collections.Generic;
using System.Threading.Tasks;
using MyAppBack.Data.Spec;
using MyAppBack.Models;
using MyAppBack.Models.OrderAggregate;

namespace MyAppBack.Data.Repos.GenericRepository
{
  public interface IGenericRepository<T> where T : BaseEntity
  {
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

    Task<int> CountAsync(ISpecification<T> spec);

    // Products
    Task<Product> GetByGuIdAsync(int guiId);


    // Orders
    Task<Order> GetOrderByPaymentIntent(string paymentIntentId);



    void Add(T entity);
    T Update(T entity);
    void Delete(T entity);
    Task<T> AddEntity(T entity);



  }
}