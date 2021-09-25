using System.Threading.Tasks;
using NonameStore.App.WebAPI.Models.Contracts;
using NonameStore.App.WebAPI.Models.External.Contracts;

namespace NonameStore.App.WebAPI.Services
{
  public interface IFacebookAuthService
  {
    Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
    Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
    Task<AccesssTokenResponse> GetAccessTokenAsync(string code);

  }
}