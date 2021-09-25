using System;
using MyAppBack.Helpers;
using MyAppBack.Models;

namespace MyAppBack.Data.Spec
{
  public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
  {
    public ProductWithFiltersForCountSpecification(UserParams userParams)
          : base(x =>
          (string.IsNullOrEmpty(userParams.Search) || x.Name.ToLower().Contains(userParams.Search.ToLower())) &&
          (!userParams.regionId.HasValue || x.ProductRegionId == userParams.regionId) &&
          (!userParams.typeId.HasValue || x.ProductTypeId == userParams.typeId) &&
          (userParams.IsAdmin == true || x.IsSale == true)

        )
    {

    }

  }

}