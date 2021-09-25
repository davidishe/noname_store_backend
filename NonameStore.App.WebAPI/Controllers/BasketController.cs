using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAppBack.Data.Repos.BasketRepository;
using MyAppBack.Dtos;
using MyAppBack.Errors;
using MyAppBack.Extensions;
using MyAppBack.Models;

namespace MyAppBack.Controllers
{

  [AllowAnonymous]
  public class BasketController : BaseApiController
  {
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;
    public BasketController(IBasketRepository basketRepository, IMapper mapper)
    {
      _mapper = mapper;
      _basketRepository = basketRepository;
    }

    [HttpGet]
    public async Task<ActionResult<Basket>> GetBasketById(string id)
    {
      var basket = await _basketRepository.GetBasketAsync(id);
      return Ok(basket ?? new Basket(id));
    }

    [HttpPost]
    public async Task<ActionResult<Basket>> UpdateBasket(BasketDto basketDto)
    {
      var basket = _mapper.Map<BasketDto, Basket>(basketDto);
      var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);
      return Ok(updatedBasket);
    }

    [HttpDelete]
    [AllowAnonymous]
    public async Task DeleteBasketAsync(string id)
    {
      await _basketRepository.DeleteBasketAsync(id);
    }

  }
}