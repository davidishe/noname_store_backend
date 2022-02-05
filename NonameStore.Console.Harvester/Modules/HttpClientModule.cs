using System;
using System.IO;
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
      var stream = await response.Content.ReadAsStreamAsync(cts.Token);
      return content;
    }


    public async Task<string> MakeHttpCallWithStream(string requestUrl)
    {
      using var httpClient = new HttpClient();
      using var response = await httpClient.GetAsync(requestUrl, cts.Token);
      var stream = await response.Content.ReadAsStreamAsync(cts.Token);


      DirectoryInfo info = new DirectoryInfo("./");
      if (!info.Exists)
      {
        info.Create();
      }

      string path = Path.Combine("./", "result.txt");
      using var outputFileStream = new FileStream(path, FileMode.Create);
      stream.CopyTo(outputFileStream);

      var content = await response.Content.ReadAsStringAsync(cts.Token);
      return content;

    }






  }
}

