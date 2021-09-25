using System.Collections.Generic;
using System.Threading.Tasks;
using MyAppBack.Helpers;
using MyAppBack.Models;

namespace MyAppBack.Data.Repos
{
  public interface IProductRepository
  {
    void Add<T>(T entity) where T : class;
    Task<bool> SaveAll();
    void Delete<T>(T entity) where T : class;
    Task<Product> GetProductByIdAsync(int id);
    Task<PagedList<Product>> GetProducts(UserParams userParams);
    Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    Task<IReadOnlyList<ProductType>> CreateProductTypeAsync(ProductType productType);
    Task<IReadOnlyList<ProductRegion>> GetProductRegionsAsync();
    Task<IReadOnlyList<ProductRegion>> CreateProductRegionAsync(ProductRegion productRegion);

  }
}