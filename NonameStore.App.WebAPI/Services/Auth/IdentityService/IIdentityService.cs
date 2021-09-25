using System.Threading.Tasks;
using MyAppBack.Domain;
using MyAppBack.Models.Contracts;

namespace MyAppBack.Services
{
  public interface IIdentityService
  {

    Task<ExternalAuthResult> LoginWithFacebookAsync(string accessToken);
    Task<ExternalAuthResult> LoginWithGoogleAsync(string accessToken);


    Task<AccesssTokenResponse> GetFacebookAccessToken(string code);
    Task<AccesssTokenResponse> GetGoogleAccessToken(string code);

  }
}