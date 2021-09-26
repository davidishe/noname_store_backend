using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NonameStore.Admin.Database;
using NonameStore.Admin.WebAPI.Models.Dtos;
using NonameStore.Admin.WebAPI.Models.Models;

namespace NonameStore.Admin.WebAPI.Controllers
{
  [ApiController]
  [AllowAnonymous]
  [Route("[controller]")]
  public class OrdersController : ControllerBase
  {

    private readonly ILogger<OrdersController> _logger;
    private readonly IDbRepository<Order> _orderRepo;
    private readonly IDbRepository<DeliveryMethod> _deliveryMethodsRepo;


    public OrdersController(ILogger<OrdersController> logger, IDbRepository<Order> orderRepo, IDbRepository<DeliveryMethod> deliveryMethodsRepo)
    {
      _logger = logger;
      _orderRepo = orderRepo;
      _deliveryMethodsRepo = deliveryMethodsRepo;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> CreateOrder(Order order)
    {
      _logger.LogInformation($"{order}");


      var deliveryMethod = await _deliveryMethodsRepo.GetByIdAsync(order.DeliveryMethod.Id);
      var subtotal = order.OrderItems.Sum(item => item.Price * item.Quantity);

      var orderToCreate = new Order(order.ByerEmail, order.ShipToAddress, deliveryMethod, order.OrderItems, order.Subtotal, order.PaymentIntentId, order.OrderNumber, order.PaymentMethod);
      await _orderRepo.AddAsync(orderToCreate);
      return Ok(200);
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult> GetAll()
    {
      var entitys = await _orderRepo.GetAll().ToListAsync();
      return Ok(entitys);
    }
  }
}
