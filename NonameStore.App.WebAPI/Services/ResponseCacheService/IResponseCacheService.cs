using System;
using System.Threading.Tasks;

namespace NonameStore.App.WebAPI.Services.ResponseCacheService
{
  public interface IResponseCacheService
  {
    Task CacheResponseService(string cacheKey, object response, TimeSpan timeToLive);
    Task<string> GetCacheResponseAsync(string cacheKey);
  }
}