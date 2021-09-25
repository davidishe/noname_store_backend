using AutoMapper;
using Microsoft.Extensions.Configuration;
using NonameStore.App.WebAPI.Dtos;
using NonameStore.App.WebAPI.Models.OrderAggregate;

namespace NonameStore.App.WebAPI.Helpers
{
  public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
  {
    private readonly IConfiguration _config;

    public OrderItemUrlResolver(IConfiguration config)
    {
      _config = config;
    }


    public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
    {
      // TODO: сделать чтобы возвращалась ссылка на домен
      if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
      {
        return _config.GetSection("ApiUrl").Value + "Assets/Images/Products/" + source.ItemOrdered.PictureUrl;
      }
      return null;
    }
  }
}