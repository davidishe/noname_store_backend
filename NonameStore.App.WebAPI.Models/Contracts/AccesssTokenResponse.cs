using Newtonsoft.Json;

namespace NonameStore.App.WebAPI.Models.Contracts
{
  public class AccesssTokenResponse
  {
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
  }
}