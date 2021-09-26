using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NonameStore.Admin.WebAPI.Models.Dtos;

namespace NonameStore.Admin.WebAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OrdersController : ControllerBase
  {

    private readonly ILogger<OrdersController> _logger;

    public OrdersController(ILogger<OrdersController> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrder(OrderDto orderDto)
    {
      _logger.LogInformation($"{orderDto}");
      Console.WriteLine("11312312312313123123123123123123123123123123123123123123123123123");
      Console.WriteLine("11312312312313123123123123123123123123123123123123123123123123123");
      Console.WriteLine("11312312312313123123123123123123123123123123123123123123123123123");
      Console.WriteLine("11312312312313123123123123123123123123123123123123123123123123123");
      return Ok("Game over");
    }
  }
}
