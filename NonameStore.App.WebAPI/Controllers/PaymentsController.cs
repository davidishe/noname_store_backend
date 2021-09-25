using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NonameStore.App.WebAPI.Models;
using NonameStore.App.WebAPI.Models.Payment;
using NonameStore.App.WebAPI.Services.PaymentService;
using Order = NonameStore.App.WebAPI.Models.OrderAggregate.Order;


namespace NonameStore.App.WebAPI.Controllers
{
  [AllowAnonymous]
  public class PaymentsController : BaseApiController
  {
    private readonly IPaymentService _paymentService;
    private readonly ILogger<IPaymentService> _logger;
    private readonly IHttpContextAccessor _accessor;

    public PaymentsController(IPaymentService paymentService, ILogger<IPaymentService> logger, IConfiguration config, IHttpContextAccessor accessor)
    {
      _paymentService = paymentService;
      _logger = logger;
      _accessor = accessor;
    }

    // static readonly Client _client = new Client("501156", "test_As0OONRn1SsvFr0IVlxULxst5DBIoWi_tyVaezSRTEI");


    [Authorize]
    [HttpPost]
    [Route("intent")]
    public async Task<ActionResult<Basket>> CreateOrUpdatePaymentIntent([FromQuery] string basketId)
    {
      var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);
      if (basket == null) return BadRequest(new Errors.ApiResponse(400, "Какая-то проблема с корзиной"));
      return basket;
    }


    [AllowAnonymous]
    [HttpPost]
    [Route("invoice")]
    public async Task<IActionResult> CreateOrUpdatePaymentInvoice(string basketId)
    {

      var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

      if (basket == null) return BadRequest(new Errors.ApiResponse(400, "Какая-то проблема с корзиной"));
      return Ok(basket);


    }


    [AllowAnonymous]
    [HttpPost]
    [Route("webhook")]
    public async Task<ActionResult> OrderWebhook()
    {

      var shopId = "734144";
      var secretKey = "test_uByrETJOaWo_XyKEaQNFIkVpw5CkNUn6G9rHV3nDCtQ";
      var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
      var client = new Client(shopId, secretKey);

      // 3. Дождитесь получения уведомления
      Message message = Client.ParseMessage(HttpContext.Request.Method, Request.ContentType, json);
      Payment payment = message?.Object;

      // if (message?.Event == Event.PaymentWaitingForCapture && payment.Paid)
      // {
      //   // 4. Подтвердите готовность принять платеж
      //   client.CapturePayment(payment.Id);
      // }

      Order order;

      switch (message.Event.ToString())
      {
        case "PaymentSucceeded":
          _logger.LogInformation("Платеж успешно проведен", payment.Id);
          // обновить статус платежа в базе
          order = await _paymentService.UpdateOrderPaymentSucceded(payment.Id);
          _logger.LogInformation("Информация об успешно платеже записана в базу данных", order.Id);
          break;
        case "payment_intent.payment_failed":
          _logger.LogInformation("Платеж не был проведен", payment.Id);
          // обновить статус платежа в базе
          order = await _paymentService.UpdateOrderPaymentFailed(payment.Id);
          _logger.LogInformation("Информация о неуспешном платеже записана в базу данных", order.Id);
          break;
      }
      return new EmptyResult();
    }

  }
}