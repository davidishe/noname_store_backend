using System;
using System.Net.Http;

namespace NonameStore.Console.Harvester
{
  class Program
  {
    static void Main(string[] args)
    {

      var requestUrl = "http://localhost:6016/api/types/all";
      var client = new HttpClientModule();
      var result = client.MakeHttpCallWithStream(requestUrl).Result;
      System.Console.BackgroundColor = ConsoleColor.White;
      System.Console.ForegroundColor = ConsoleColor.Black;
      System.Console.WriteLine(result);

    }
  }
}

