using System;
using MyAppBack.Helpers;
using MyAppBack.Models;

namespace MyAppBack.Data.Spec
{
  public class ProductsWithTypesAndRegionsSpecification : BaseSpecification<Product>
  {
    public ProductsWithTypesAndRegionsSpecification(UserParams userParams)
    : base(x =>
          (string.IsNullOrEmpty(userParams.Search) || x.Name.ToLower().Contains(userParams.Search.ToLower())) &&
          (!userParams.regionId.HasValue || x.ProductRegionId == userParams.regionId) &&
          (!userParams.typeId.HasValue || x.ProductTypeId == userParams.typeId) &&
          (userParams.IsAdmin == true || x.IsSale == true)
        )
    {
      AddInclude(x => x.Type);
      AddInclude(x => x.Region);
      ApplyPaging((userParams.PageSize * (userParams.PageIndex)), userParams.PageSize);

      if (!string.IsNullOrEmpty(userParams.sort))
      {
        switch (userParams.sort)
        {
          case "priceAsc":
            AddOrderByAscending(p => p.Price);
            break;
          case "priceDesc":
            AddOrderByDescending(s => s.Price);
            break;
          case "name":
            AddOrderByAscending(s => s.Name);
            break;
          default:
            AddOrderByDescending(x => x.EnrolledDate);
            break;
        }
      }
    }

    public ProductsWithTypesAndRegionsSpecification(int id) : base(x => x.Id == id)
    {
      AddInclude(x => x.Type);
      AddInclude(x => x.Region);
    }
  }


}