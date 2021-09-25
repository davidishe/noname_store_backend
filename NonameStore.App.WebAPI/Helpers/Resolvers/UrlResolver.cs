using AutoMapper;
using Microsoft.Extensions.Configuration;
using NonameStore.App.WebAPI.Dtos;
using NonameStore.App.WebAPI.Models;

namespace NonameStore.App.WebAPI.Helpers
{
  public class UrlResolver : IValueResolver<Product, ProductToReturnDto, string>
  {
    public UrlResolver()
    {
    }

    private readonly IConfiguration _config;

    public UrlResolver(IConfiguration config)
    {
      _config = config;
    }

    public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
    {

      // TODO: сделать чтобы возвращалась ссылка на домен
      if (!string.IsNullOrEmpty(source.PictureUrl))
      {
        return _config.GetSection("ApiUrl").Value + "Assets/Images/Products/" + source.PictureUrl;
      }
      return null;
    }
  }

}