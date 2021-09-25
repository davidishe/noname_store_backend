using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyAppBack.Data.Spec;
using MyAppBack.Models;

namespace MyAppBack.Data.Config
{
  public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
  {
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
      var query = inputQuery;
      if (spec.Criteria != null)
      {
        query = query.Where(spec.Criteria);
      }

      if (spec.OrderByAscending != null)
      {
        query = query.OrderBy(spec.OrderByAscending);
      }

      if (spec.OrderByDescending != null)
      {
        query = query.OrderByDescending(spec.OrderByDescending);
      }

      if (spec.IsPagingEnabled)
      {
        query = query.Skip(spec.Skip).Take(spec.Take);
      }

      query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
      return query;
    }

  }
}