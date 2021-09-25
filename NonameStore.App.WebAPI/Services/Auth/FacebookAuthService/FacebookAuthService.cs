using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyAppBack.External.Contracts;
using MyAppBack.Options;
using MyAppBack.Models.Contracts;
using Microsoft.Extensions.Configuration;

namespace MyAppBack.Services
{
  public class FacebookAuthService : IFacebookAuthService
  {
    private const string TokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
    private const string UserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";

    private const string AccessTokenInfo = "https://graph.facebook.com/v8.0/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}";


    private readonly FacebookAuthSettings _facebookAuthSettings;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _redirectUri;

    public FacebookAuthService(IConfiguration config, FacebookAuthSettings facebookAuthSettings, IHttpClientFactory httpClientFactory)
    {
      _facebookAuthSettings = facebookAuthSettings;
      _httpClientFactory = httpClientFactory;
      _redirectUri = config.GetSection("FacebookAuthSettings:RedirectUri").Value;

    }

    public async Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken)
    {
      var formattedUrl = string.Format(TokenValidationUrl, accessToken, _facebookAuthSettings.AppId,
          _facebookAuthSettings.AppSecret);
      var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
      result.EnsureSuccessStatusCode();
      var responseAsString = await result.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);
    }

    public async Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken)
    {
      var formattedUrl = string.Format(UserInfoUrl, accessToken);
      var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
      result.EnsureSuccessStatusCode();
      var responseAsString = await result.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsString);
    }

    public async Task<AccesssTokenResponse> GetAccessTokenAsync(string code)
    {
      var formattedUrl = string.Format(AccessTokenInfo, _facebookAuthSettings.AppId, _redirectUri, _facebookAuthSettings.AppSecret, code);
      var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
      var responseAsString = await result.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<AccesssTokenResponse>(responseAsString);



    }
  }
}