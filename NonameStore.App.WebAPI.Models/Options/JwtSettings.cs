using System;

namespace NonameStore.App.WebAPI.Models.Options
{
  public class JwtSettings
  {
    public string Secret { get; set; }

    public TimeSpan TokenLifetime { get; set; }
  }
}