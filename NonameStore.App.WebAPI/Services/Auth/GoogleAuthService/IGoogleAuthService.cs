using System.Threading.Tasks;
using NonameStore.App.WebAPI.Models.Contracts;
using NonameStore.App.WebAPI.Models.Contracts.Google;
using NonameStore.App.WebAPI.Models.External.Contracts;

namespace NonameStore.App.WebAPI.Services
{
  public interface IGoogleAuthService
  {
    Task<GoogleTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
    Task<GoogleUserInfoResult> GetUserInfoAsync(string accessToken);
    Task<AccesssTokenResponse> GetAccessTokenAsync(string code);

  }
}