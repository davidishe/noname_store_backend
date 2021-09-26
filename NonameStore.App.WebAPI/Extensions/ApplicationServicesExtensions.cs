using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NonameStore.App.Domains.OrderCreator;
using NonameStore.App.WebAPI.Data.Repos;
using NonameStore.App.WebAPI.Data.Repos.BasketRepository;
using NonameStore.App.WebAPI.Data.Repos.GenericRepository;
using NonameStore.App.WebAPI.Data.UnitOfWork;
using NonameStore.App.WebAPI.Errors;
using NonameStore.App.WebAPI.Services;
using NonameStore.App.WebAPI.Services.OrderService;
using NonameStore.App.WebAPI.Services.PaymentService;
using NonameStore.App.WebAPI.Services.ResponseCacheService;
using NonameStore.App.WebAPI.Services.TokenService;

namespace NonameStore.App.WebAPI.Extensions
{
  public static class ApplicationServicesExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
      services.AddSingleton<IResponseCacheService, ResponseCacheService>();
      services.AddScoped<ITokenService, TokenService>();
      services.AddScoped<IPaymentService, PaymentService>();
      services.AddScoped<IOrderService, OrderService>();

      services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped<IBasketRepository, BasketRepository>();
      services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IOrderCreator, OrderCreator>();


      services.Configure<ApiBehaviorOptions>(options =>
      {
        options.InvalidModelStateResponseFactory = actionContext =>
        {
          var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage).ToArray();

          var errorResponse = new ApiValidationErrorResponse()
          {
            Errors = errors
          };

          return new BadRequestObjectResult(errorResponse);

        };

      });

      return services;

    }
  }
}