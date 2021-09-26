using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NonameStore.Admin.WebAPI.Models.Models;

namespace NonameStore.Admin.Database
{
  public class DataContextSeed
  {

    public static async Task SeedDataAsync(DataContext context, ILoggerFactory loggerFactory)
    {
      try
      {

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);


        if (!context.DeliveryMethods.Any())
        {
          var itemsData = File.ReadAllText(path + @"/SeedData/delivery.json");
          var items = JsonSerializer.Deserialize<List<DeliveryMethod>>(itemsData);
          foreach (var item in items)
          {
            context.DeliveryMethods.Add(item);
          }
          await context.SaveChangesAsync();
        }

      }
      catch (Exception ex)
      {
        var logger = loggerFactory.CreateLogger<DataContextSeed>();
        logger.LogError(ex.Message);
      }
    }


  }
}