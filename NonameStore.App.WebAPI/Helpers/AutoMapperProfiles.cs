using AutoMapper;
using NonameStore.App.WebAPI.Dtos;
using NonameStore.App.WebAPI.Identity;
using NonameStore.App.WebAPI.Models;
using NonameStore.App.WebAPI.Models.Dtos;
using NonameStore.App.WebAPI.Models.Identity;
using NonameStore.App.WebAPI.Models.OrderAggregate;

namespace NonameStore.App.WebAPI.Helpers
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

      CreateMap<NonameStore.App.WebAPI.Models.Identity.Address, AddressDto>().ReverseMap();
      CreateMap<BasketDto, Basket>();

      CreateMap<BasketItemDto, BasketItem>();
      CreateMap<AddressDto, NonameStore.App.WebAPI.Models.OrderAggregate.Address>();
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