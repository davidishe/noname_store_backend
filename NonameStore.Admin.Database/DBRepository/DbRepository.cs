using System.Linq;
using System.Threading.Tasks;
using NonameStore.Admin.WebAPI.Models;

namespace NonameStore.Admin.Database
{
  public class DbRepository<TEntity> : IDbRepository<TEntity> where TEntity : BaseEntity
  {
    private readonly DataContext _context;

    public DbRepository(DataContext context)
    {
      _context = context;
    }

    /// <inheritdoc />
    public IQueryable<TEntity> GetAll()
    {
      return _context.Set<TEntity>().AsQueryable();
    }

    /// <inheritdoc />
    public async Task<TEntity> AddAsync(TEntity entity)
    {
      await _context.Set<TEntity>().AddAsync(entity);
      await _context.SaveChangesAsync();
      return entity;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TEntity entity)
    {
      await Task.Run(() => _context.Set<TEntity>().Update(entity));
      await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(TEntity entity)
    {
      await Task.Run(() => _context.Set<TEntity>().Remove(entity));
      await _context.SaveChangesAsync();
    }


    /// <inheritdoc />
    public async Task<TEntity> GetByIdAsync(int id)
    {
      var entity = await Task.Run(() => _context.Set<TEntity>().Where(x => x.Id == id).FirstOrDefault());
      await _context.SaveChangesAsync();
      return entity;
    }


  }
}