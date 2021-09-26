using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NonameStore.App.WebAPI.Models.OrderAggregate;

namespace NonameStore.App.Domains.OrderCreator
{
  public class OrderCreator : IOrderCreator
  {


    private readonly IHttpClientFactory _clientFactory;
    private readonly string _remoteServiceBaseUrl = "https://localhost:6017/orders/create";
    private readonly ILogger<OrderCreator> _logger;

    public OrderCreator(IHttpClientFactory clientFactory, ILogger<OrderCreator> logger)
    {
      _clientFactory = clientFactory;
      _logger = logger;
    }

    public async Task<bool> CreateOrderInDatabase(Order order)
    {

      var request = new HttpRequestMessage(HttpMethod.Post, _remoteServiceBaseUrl);
      request.Content = new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json");

      request.Headers.Add("Accept", "application/vnd.github.v3+json");
      var client = _clientFactory.CreateClient();
      HttpResponseMessage response = await client.SendAsync(request);
      if (response.IsSuccessStatusCode)
      {
        using var responseStream = await response.Content.ReadAsStreamAsync();
        var resultResponse = JsonSerializer.DeserializeAsync<object>(responseStream).Result;
      }
      else
      {
        _logger.LogError("Ошибка при получении ответа из административной панели");
        // ошибка при получении ответа
      }
      return true;
    }


  }
}