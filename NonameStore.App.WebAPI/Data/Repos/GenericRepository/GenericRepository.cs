using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NonameStore.App.WebAPI.Data.Config;
using NonameStore.App.WebAPI.Data.Spec;
using NonameStore.App.WebAPI.Infrastructure.Database;
using NonameStore.App.WebAPI.Models;
using NonameStore.App.WebAPI.Models.OrderAggregate;

namespace NonameStore.App.WebAPI.Data.Repos.GenericRepository
{
  public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
  {

    private readonly AppDbContext _context;
    public GenericRepository(AppDbContext context)
    {
      _context = context;
    }


    #region generic methods
    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
      return await _context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
      var result = ApplySpecification(spec);
      var resultToReturn = await result.ToListAsync();
      return resultToReturn;
    }

    public async Task<T> GetByIdAsync(int id)
    {
      return await _context.Set<T>().FindAsync(id);
    }



    public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
    {
      return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public void Add(T entity)
    {
      _context.Set<T>().Add(entity);
      SaveChanges();
    }

    public async Task<T> AddEntity(T entity)
    {
      await _context.Set<T>().AddAsync(entity);
      SaveChanges();
      return entity;
    }

    public T Update(T entity)
    {
      _context.Set<T>().Attach(entity);
      _context.Entry(entity).State = EntityState.Modified;
      SaveChanges();
      return entity;
    }

    void IGenericRepository<T>.Delete(T entity)
    {
      _context.Set<T>().Remove(entity);
      SaveChanges();
    }

    private void SaveChanges()
    {
      _context.SaveChanges();
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
      return await ApplySpecification(spec).CountAsync();
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
      var result = SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
      return result;
    }

    #endregion




    public async Task<Product> GetByGuIdAsync(int guiId)
    {
      return await _context.Products.Where(x => x.GuId == guiId).FirstOrDefaultAsync();
    }

    public async Task<Order> GetOrderByPaymentIntent(string paymentIntentId)
    {
      var order = await _context.Orders.Where(x => x.PaymentIntentId == paymentIntentId).FirstOrDefaultAsync();
      return order;
    }



    public async Task<IReadOnlyList<ProductType>> CreateProductTypeAsync(ProductType productType)
    {
      _context.Add(productType);
      SaveChanges();
      var types = _context.ProductTypes;
      return await types.ToListAsync();
    }

    // public async Task<IReadOnlyList<ProductRegion>> CreateProductRegionAsync(ProductRegion productRegion)
    // {
    //   _context.Add(productRegion);
    //   _context.SaveChanges();
    //   var regions = _context.ProductRegions;
    //   return await regions.ToListAsync();
    // }

  }
}