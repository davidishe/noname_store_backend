using System.Threading.Tasks;
using MyAppBack.External.Contracts;
using MyAppBack.Models.Contracts;

namespace MyAppBack.Services
{
  public interface IFacebookAuthService
  {
    Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
    Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
    Task<AccesssTokenResponse> GetAccessTokenAsync(string code);

  }
}