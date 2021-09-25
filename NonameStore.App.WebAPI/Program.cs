using System;
using System.Threading.Tasks;
using Identity.Database.SeedData;
using Infrastructure.Database.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyAppBack.Identity;
using MyAppBack.Identity.Database;
using MyAppBack.Infrastructure.Database;
using MyAppBack.Models.Identity;

namespace MyAppBack
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      using (var scope = host.Services.CreateScope())
      {

        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
          var context = services.GetRequiredService<AppDbContext>();
          await DataContextSeed.SeedDataAsync(context, loggerFactory);
          await context.Database.MigrateAsync();

          var userManager = services.GetRequiredService<UserManager<AppUser>>();
          var roleManager = services.GetRequiredService<RoleManager<Role>>();

          var identityContext = services.GetRequiredService<IdentityContext>();
          await IdentityContextSeed.SeedUsersAsync(userManager, roleManager, loggerFactory);
          await identityContext.Database.MigrateAsync();


        }
        catch (Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "An error occured during migrtation");
        }
      }

      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>().
                UseUrls("https://localhost:6016");
            });
  }
}
