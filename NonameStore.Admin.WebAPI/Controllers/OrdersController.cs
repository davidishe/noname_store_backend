using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

    public OrdersController(ILogger<OrdersController> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> CreateOrder(Order order)
    {
      _logger.LogInformation($"{order}");
      Console.WriteLine("11312312312313123123123123123123123123123123123123123123123123123");
      Console.WriteLine("11312312312313123123123123123123123123123123123123123123123123123");
      Console.WriteLine("11312312312313123123123123123123123123123123123123123123123123123");
      Console.WriteLine("11312312312313123123123123123123123123123123123123123123123123123");
      return Ok("Game over");
    }
  }
}
