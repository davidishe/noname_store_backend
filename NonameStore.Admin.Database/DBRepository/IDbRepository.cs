using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NonameStore.Admin.WebAPI.Models;

namespace NonameStore.Admin.Database
{
  public interface IDbRepository<TEntity> where TEntity : BaseEntity
  {
    IQueryable<TEntity> GetAll();
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(int id);

  }
}
