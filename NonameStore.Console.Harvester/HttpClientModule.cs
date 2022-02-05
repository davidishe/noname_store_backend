using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NonameStore.Console.Harvester
{
  public class HttpClientModule
  {

    private readonly CancellationTokenSource cts = new();

    public async Task<string> MakeHttpCall(string requestUrl)
    {
      using var httpClient = new HttpClient();
      using var response = await httpClient.GetAsync(requestUrl, cts.Token);
      var content = await response.Content.ReadAsStringAsync(cts.Token);
      return content;
    }



  }
}

