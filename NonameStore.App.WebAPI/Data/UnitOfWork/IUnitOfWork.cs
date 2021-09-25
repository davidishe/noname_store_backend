using System;
using System.Threading.Tasks;
using NonameStore.App.WebAPI.Data.Repos.GenericRepository;
using NonameStore.App.WebAPI.Models;

namespace NonameStore.App.WebAPI.Data.UnitOfWork
{
  public interface IUnitOfWork : IDisposable
  {
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> Complete();
  }
}