using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyAppBack.Helpers;
using MyAppBack.Infrastructure.Database;
using MyAppBack.Models;

namespace MyAppBack.Data.Repos
{
  public class ProductRepository : IProductRepository
  {

    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
      _context = context;
    }

    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
      var product = await _context.Products.Include(p => p.Id).FirstOrDefaultAsync();
      return product;
    }

    public async Task<bool> SaveAll()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public async Task<PagedList<Product>> GetProducts(UserParams userParams)
    {
      var products = _context.Products;
      return await PagedList<Product>.CreateAsync(products, userParams.PageIndex, userParams.PageSize);
    }

    public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
    {
      return await _context.ProductTypes.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductRegion>> GetProductRegionsAsync()
    {
      return await _context.Regions.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> CreateProductTypeAsync(ProductType productType)
    {
      // var types = _context.Types;
      _context.Add(productType);
      _context.SaveChanges();
      return await _context.ProductTypes.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductRegion>> CreateProductRegionAsync(ProductRegion productRegion)
    {
      // var types = _context.Regions;
      _context.Add(productRegion);
      _context.SaveChanges();
      return await _context.Regions.ToListAsync();
    }
  }
}