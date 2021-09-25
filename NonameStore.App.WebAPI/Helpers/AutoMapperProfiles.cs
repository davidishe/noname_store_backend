using AutoMapper;
using MyAppBack.Dtos;
using MyAppBack.Identity;
using MyAppBack.Models;
using MyAppBack.Models.OrderAggregate;

namespace MyAppBack.Helpers
{
  public class AutoMapperProfiles : Profile
  {

    public AutoMapperProfiles()
    {
      CreateMap<AppUser, UserToReturnDto>();

      CreateMap<Product, ProductToReturnDto>()
        .ForMember(d => d.Type, m => m.MapFrom(s => s.Type.Name))
        .ForMember(d => d.Region, m => m.MapFrom(s => s.Region.Name))
        .ForMember(d => d.PictureUrl, m => m.MapFrom<UrlResolver>());

      CreateMap<MyAppBack.Identity.Address, AddressDto>().ReverseMap();
      CreateMap<BasketDto, Basket>();

      CreateMap<BasketItemDto, BasketItem>();
      CreateMap<AddressDto, MyAppBack.Models.OrderAggregate.Address>();
      CreateMap<Order, OrderToReturnDto>()
        .ForMember(d => d.DeliveryMethod, m => m.MapFrom(s => s.DeliveryMethod.ShortName))
        .ForMember(d => d.DeliveryPrice, m => m.MapFrom(s => s.DeliveryMethod.Price));

      CreateMap<OrderItem, OrderItemDto>()
        .ForMember(d => d.ProductId, m => m.MapFrom(s => s.ItemOrdered.ProductItemId))
        .ForMember(d => d.Name, m => m.MapFrom(s => s.ItemOrdered.Name))
        .ForMember(d => d.GuId, m => m.MapFrom(s => s.ItemOrdered.GuId))
        .ForMember(d => d.PictureUrl, m => m.MapFrom(s => s.ItemOrdered.PictureUrl))
        .ForMember(d => d.PictureUrl, m => m.MapFrom<OrderItemUrlResolver>());

    }
  }
}