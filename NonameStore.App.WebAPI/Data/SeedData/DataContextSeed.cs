using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NonameStore.App.WebAPI.Infrastructure.Database;
using NonameStore.App.WebAPI.Models;

namespace Infrastructure.Database.SeedData
{
  public class DataContextSeed
  {

    public static async Task SeedDataAsync(AppDbContext context, ILoggerFactory loggerFactory)
    {
      try
      {

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (!context.Regions.Any())
        {
          var itemsData = File.ReadAllText(path + @"/Data/SeedData/Source/regions.json");
          var items = JsonSerializer.Deserialize<List<ProductRegion>>(itemsData);
          foreach (var item in items)
          {
            context.Regions.Add(item);
          }
          await context.SaveChangesAsync();
        }

        if (!context.ProductTypes.Any())
        {
          var itemsData = File.ReadAllText(path + @"/Data/SeedData/Source/types.json");
          var items = JsonSerializer.Deserialize<List<ProductType>>(itemsData);
          foreach (var item in items)
          {
            context.ProductTypes.Add(item);
          }
          await context.SaveChangesAsync();
        }




        if (!context.Products.Any())
        {
          var itemsData = File.ReadAllText(path + @"/Data/SeedData/Source/products.json");
          var items = JsonSerializer.Deserialize<List<Product>>(itemsData);


          foreach (var item in items)
          {
            context.Products.Add(item);
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