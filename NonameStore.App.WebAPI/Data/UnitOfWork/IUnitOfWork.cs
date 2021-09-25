using System;
using System.Threading.Tasks;
using MyAppBack.Data.Repos.GenericRepository;
using MyAppBack.Models;

namespace MyAppBack.Data.UnitOfWork
{
  public interface IUnitOfWork : IDisposable
  {
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<int> Complete();
  }
}