using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MyAppBack.Identity;
using MyAppBack.Models.Identity;
using MyAppBack.Identity.Database;

namespace Identity.Database.SeedData
{
  public class IdentityContextSeed
  {
    public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<Role> roleManager, ILoggerFactory loggerFactory)
    {
      var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

      try
      {

        if (!userManager.Users.Any())
        {

          var itemsData = File.ReadAllText(path + @"/Data/SeedData/Source/users.json");
          var users = JsonSerializer.Deserialize<List<AppUser>>(itemsData);
          foreach (var user in users)
          {
            var result = userManager.CreateAsync(user, "Pa$$w0rd").Result;
          }

        }

        if (!roleManager.Roles.Any())
        {
          var roles = new List<Role>()
          {
            new Role {Name = "User" },
            new Role {Name = "Admin" },
            new Role {Name = "Curator"},

          };

          foreach (var role in roles)
          {
            await roleManager.CreateAsync(role);
          }

          var admin = userManager.FindByNameAsync("admin@admin.ru").Result;
          await userManager.AddToRolesAsync(admin, new[] { "Admin", "User" });

          var curator = userManager.FindByNameAsync("curator@curator.ru").Result;
          await userManager.AddToRolesAsync(curator, new[] { "Curator", "User" });

        }



      }
      catch (Exception ex)
      {
        var logger = loggerFactory.CreateLogger<IdentityContextSeed>();
        logger.LogError(ex.Message);
      }
    }
  }
}