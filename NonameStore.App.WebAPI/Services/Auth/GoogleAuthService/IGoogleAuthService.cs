using System.Threading.Tasks;
using MyAppBack.External.Contracts;
using MyAppBack.Models.Contracts;
using MyAppBack.Models.Contracts.Google;

namespace MyAppBack.Services
{
  public interface IGoogleAuthService
  {
    Task<GoogleTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
    Task<GoogleUserInfoResult> GetUserInfoAsync(string accessToken);
    Task<AccesssTokenResponse> GetAccessTokenAsync(string code);

  }
}