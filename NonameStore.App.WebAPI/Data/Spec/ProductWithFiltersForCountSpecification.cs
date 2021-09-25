using System;
using NonameStore.App.WebAPI.Helpers;
using NonameStore.App.WebAPI.Models;

namespace NonameStore.App.WebAPI.Data.Spec
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