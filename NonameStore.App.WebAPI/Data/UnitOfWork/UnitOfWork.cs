using System;
using System.Collections;
using System.Threading.Tasks;
using NonameStore.App.WebAPI.Data.Repos.GenericRepository;
using NonameStore.App.WebAPI.Infrastructure.Database;
using NonameStore.App.WebAPI.Models;

namespace NonameStore.App.WebAPI.Data.UnitOfWork
{
  public class UnitOfWork : IUnitOfWork
  {
    private Hashtable _repositories;
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
      _context = context;
    }

    public async Task<int> Complete()
    {
      return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
      _context.Dispose();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
      if (_repositories == null) _repositories = new Hashtable();
      var type = typeof(TEntity).Name;
      if (!_repositories.ContainsKey(type))
      {
        var repositoryType = typeof(GenericRepository<>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
        _repositories.Add(type, repositoryInstance);
      }

      return (IGenericRepository<TEntity>)_repositories[type];

    }
  }
}