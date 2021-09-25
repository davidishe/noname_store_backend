using AutoMapper;
using Microsoft.Extensions.Configuration;
using MyAppBack.Dtos;
using MyAppBack.Models.OrderAggregate;

namespace MyAppBack.Helpers
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