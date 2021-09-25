using Newtonsoft.Json;

namespace MyAppBack.Models.Contracts
{
  public class AccesssTokenResponse
  {
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
  }
}