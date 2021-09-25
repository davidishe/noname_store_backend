using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NonameStore.App.WebAPI.Models.Dtos;

namespace NonameStore.App.Domains.Order.OrderCreator
{
  public class OrderCreator : IOrderCreator
  {


    private readonly IHttpClientFactory _clientFactory;
    private readonly string _remoteServiceBaseUrl = "https://localhost:6017/order";

    public OrderCreator(IHttpClientFactory clientFactory)
    {
      _clientFactory = clientFactory;
    }

    public async Task<bool> CreateOrderInDatabase(OrderDto orderDto)
    {

      var request = new HttpRequestMessage(HttpMethod.Post, _remoteServiceBaseUrl);
      request.Headers.Add("Accept", "application/vnd.github.v3+json");
      var client = _clientFactory.CreateClient();
      HttpResponseMessage response = await client.SendAsync(request);
      if (response.IsSuccessStatusCode)
      {
        using var responseStream = await response.Content.ReadAsStreamAsync();
        var metricsResponse = JsonSerializer.DeserializeAsync<object>(responseStream).Result;
      }
      else
      {
        // ошибка при получении ответа
      }
      return true;
    }


  }
}