using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NonameStore.App.WebAPI.Data.Repos.GenericRepository;
using NonameStore.App.WebAPI.Data.Spec;
using NonameStore.App.WebAPI.Dtos;
using NonameStore.App.WebAPI.Dtos.Product;
using NonameStore.App.WebAPI.Helpers;
using NonameStore.App.WebAPI.Models;

namespace NonameStore.App.WebAPI.Controllers
{


  [AllowAnonymous]
  public class ProductsController : BaseApiController
  {

    private readonly IGenericRepository<Product> _productsRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;
    private readonly IGenericRepository<ProductRegion> _productRegionRepo;
    private readonly IMapper _mapper;


    public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductType> productTypeRepo, IGenericRepository<ProductRegion> productRegionRepo, IMapper mapper)
    {
      _productsRepo = productsRepo;
      _productTypeRepo = productTypeRepo;
      _productRegionRepo = productRegionRepo;
      _mapper = mapper;
    }


    #region 1. Get products functionality

    [Authorize(Policy = "RequireModerator")]
    [HttpGet]
    [Route("all/admin")]
    public async Task<ActionResult> GetAllAdmin([FromQuery] UserParams userParams)
    {

      await SetTimeOut();
      userParams.IsAdmin = true;

      var specForCount = new ProductWithFiltersForCountSpecification(userParams);
      var totalItems = await _productsRepo.CountAsync(specForCount);

      var spec = new ProductsWithTypesAndRegionsSpecification(userParams);
      var products = await _productsRepo.ListAsync(spec);
      var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
      await SetTimeOut();

      return Ok(new Pagination<ProductToReturnDto>(userParams.PageIndex, userParams.PageSize, totalItems, data));
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllClient([FromQuery] UserParams userParams)
    {

      userParams.IsAdmin = false;

      // count all items for pagination
      var specForCount = new ProductWithFiltersForCountSpecification(userParams);
      var totalItems = await _productsRepo.CountAsync(specForCount);

      // count all items for 
      var spec = new ProductsWithTypesAndRegionsSpecification(userParams);
      var products = await _productsRepo.ListAsync(spec);
      var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
      await SetTimeOut();
      return Ok(new Pagination<ProductToReturnDto>(userParams.PageIndex, userParams.PageSize, totalItems, data));
    }


    // [Cached(10)]
    [AllowAnonymous]
    [HttpGet("{id}")]
    [Route("product")]
    public async Task<ActionResult<ProductToReturnDto>> GetProductByIdAsync([FromQuery] int id)
    {
      var spec = new ProductsWithTypesAndRegionsSpecification(id);
      var product = await _productsRepo.GetEntityWithSpec(spec);
      await SetTimeOut();
      return _mapper.Map<Product, ProductToReturnDto>(product);

    }

    [AllowAnonymous]
    [HttpGet("{guId}")]
    [Route("getproductid")]
    public async Task<ActionResult<ProductToReturnDto>> GetProductIdByGuIdAsync([FromQuery] int guId)
    {
      var product = await _productsRepo.GetByGuIdAsync(guId);
      await SetTimeOut();

      var productType = _productTypeRepo.GetByIdAsync(product.ProductTypeId).Result;
      var productRegion = _productRegionRepo.GetByIdAsync(product.ProductRegionId).Result;
      product.Type = productType;
      product.Region = productRegion;



      return _mapper.Map<Product, ProductToReturnDto>(product);
    }

    #endregion

    #region 2. Get regions & types functionality
    // [Cached(600)]
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
    [HttpGet]
    [Route("regions")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductRegionsByIdAsync()
    {
      var product = await _productRegionRepo.ListAllAsync();
      await SetTimeOut();
      return Ok(product);
    }
    #endregion

    #region 3. Products CRUD functionality
    /*
    create, delete, update products
    */

    [AllowAnonymous]
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<Product>> CreateProduct(ProductToCreate productToCreate)
    {

      await SetTimeOut();


      var productToReturn = new Product
      (
        productToCreate.Name,
        productToCreate.Price,
        "",
        productToCreate.Description,
        productToCreate.ProductTypeId,
        await _productTypeRepo.GetByIdAsync(productToCreate.ProductTypeId),
        productToCreate.ProductRegionId,
        await _productRegionRepo.GetByIdAsync(productToCreate.ProductRegionId),
        productToCreate.Quantity
      );

      var product = await _productsRepo.AddEntity(productToReturn);
      return Ok(_mapper.Map<Product, ProductToReturnDto>(product));

    }


    [AllowAnonymous]
    [HttpPut]
    [Route("update")]
    public async Task<ActionResult<Product>> UpdateProduct(ProductToCreate productForUpdate)
    {

      await SetTimeOut();

      var currentProduct = await _productsRepo.GetByIdAsync((int)productForUpdate.Id);

      currentProduct.Name = productForUpdate.Name;
      currentProduct.Price = productForUpdate.Price;
      currentProduct.Quantity = productForUpdate.Quantity;
      currentProduct.Description = productForUpdate.Description;

      currentProduct.ProductRegionId = productForUpdate.ProductRegionId;
      currentProduct.Region = _productRegionRepo.GetByIdAsync(productForUpdate.ProductRegionId).Result;
      currentProduct.ProductTypeId = productForUpdate.ProductTypeId;
      currentProduct.Type = _productTypeRepo.GetByIdAsync(productForUpdate.ProductTypeId).Result;


      var updatedProduct = _productsRepo.Update(currentProduct);
      return Ok(_mapper.Map<Product, ProductToReturnDto>(updatedProduct));

    }



    [AllowAnonymous]
    [HttpPost]
    [Route("photo")]
    public async Task<ActionResult<Product>> AddPhoto(int productId)
    {

      await SetTimeOut();

      IFormFile file = Request.Form.Files[0];
      var productPictureFileName = await SaveFileToServer(file);
      Product product = await _productsRepo.GetByIdAsync(productId);
      product.PictureUrl = productPictureFileName;
      _productsRepo.Update(product);
      product = await _productsRepo.GetByIdAsync(productId);

      var productToReturn = _mapper.Map<Product, ProductToReturnDto>(product);
      return Ok(productToReturn);
    }

    [AllowAnonymous]
    [HttpPatch]
    [Route("sale")]
    public async Task<ActionResult<Product>> SaleSettings(int productId)
    {

      await SetTimeOut();
      var product = await _productsRepo.GetByIdAsync(productId);
      product.IsSale = !product.IsSale;
      _productsRepo.Update(product);
      return Ok(205);
    }

    [AllowAnonymous]
    [HttpDelete]
    [Route("delete")]
    public async Task<ActionResult<Product>> DeleteProduct(int productId)
    {

      await SetTimeOut();
      var product = await _productsRepo.GetByIdAsync(productId);
      _productsRepo.Delete(product);
      return Ok(202);
    }


    #endregion

    #region 4. Regions & types CRUD functionality

    [AllowAnonymous]
    [HttpPost]
    [Route("create-type")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> CreateProductTypeAsync(ProductType productType)
    {
      // var product = await _productsRepo.CreateProductTypeAsync(productType);
      // await SetTimeOut();
      // return Ok(product);

      //FIXME: поправить
      return Ok(200);

    }

    [AllowAnonymous]
    [HttpPost]
    [Route("create-region")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> CreateProductRegionAsync(ProductRegion productRegion)
    {
      // var product = await _productsRepo.CreateProductRegionAsync(productRegion);
      // await SetTimeOut();
      // return Ok(product);


      //FIXME: поправить
      return Ok(200);
    }

    #endregion

    #region 5. Private methods for service functionaluty
    private Task<string> SaveFileToServer(IFormFile file)
    {
      var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
      var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Images", "Products", fileName);

      using (var stream = new FileStream(fullPath, FileMode.Create))
      {
        file.CopyTo(stream);
      }

      return Task.FromResult(fileName);

    }

    private async Task<bool> SetTimeOut()
    {
      await Task.Delay(2000);
      return true;
    }

    #endregion

  }
}