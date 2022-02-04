using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NonameStore.App.WebAPI.Data.Repos.GenericRepository;
using NonameStore.App.WebAPI.Models;

namespace NonameStore.App.WebAPI.Controllers
{


  [AllowAnonymous]
  public class TypesController : BaseApiController
  {

    private readonly IGenericRepository<ProductType> _productTypeRepo;

    public TypesController(IGenericRepository<ProductType> productTypeRepo)
    {
      _productTypeRepo = productTypeRepo;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypesByIdAsync()
    {
      var product = await _productTypeRepo.ListAllAsync();
      await SetTimeOut();
      return Ok(product);
    }



    [AllowAnonymous]
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> CreateProductTypeAsync(ProductType productType)
    {
      var product = await _productTypeRepo.AddEntity(productType);
      await SetTimeOut();
      return Ok(product);
    }


    private async Task<bool> SetTimeOut()
    {
      await Task.Delay(300);
      return true;
    }


  }
}
