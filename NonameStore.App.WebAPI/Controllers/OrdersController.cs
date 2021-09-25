using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NonameStore.App.WebAPI.Dtos;
using NonameStore.App.WebAPI.Errors;
using NonameStore.App.WebAPI.Extensions;
using NonameStore.App.WebAPI.Models.Dtos;
using NonameStore.App.WebAPI.Models.OrderAggregate;
using NonameStore.App.WebAPI.Services.OrderService;

namespace NonameStore.App.WebAPI.Controllers
{

  [AllowAnonymous]
  public class OrdersController : BaseApiController
  {

    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    private readonly ILogger<OrdersController> _logger;
    public OrdersController(IOrderService orderService, IMapper mapper, ILogger<OrdersController> logger)
    {
      _mapper = mapper;
      _orderService = orderService;
      _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
    {
      var email = HttpContext.User.RetrieveEmailFromPrincipal();
      var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
      var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address, orderDto.PaymentMethod);

      if (order == null)
      {
        _logger.LogCritical("Ошибка при создании заказа");
        return BadRequest(new ApiResponse(400, "Проблема при создании заказа"));
      }

      var orderToReturnDto = _mapper.Map<Order, OrderToReturnDto>(order);
      return Ok(orderToReturnDto);

    }



    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
    {
      var email = HttpContext.User.RetrieveEmailFromPrincipal();
      var orders = await _orderService.GetOrdersForUserAsync(email);
      return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
    }

    [Route("order")]
    [HttpGet]
    public async Task<ActionResult<Order>> GetOrderByIdForUser([FromQuery] int id)
    {
      var email = HttpContext.User.RetrieveEmailFromPrincipal();
      var order = await _orderService.GetOrderById(id, email);
      if (order == null) return NotFound(new ApiResponse(404, "Такой заказ на найден"));
      return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
    }

    [HttpGet]
    [Route("deliverymethods")]
    public async Task<ActionResult<Order>> GetDeliveryMethodsAsync(int id)
    {
      var email = HttpContext.User.RetrieveEmailFromPrincipal();
      var methods = await _orderService.GetDeliveryMethodsAsync();
      if (methods == null) return NotFound(new ApiResponse(404, "Способы доставки не найдены"));
      return Ok(methods);
    }

  }
}