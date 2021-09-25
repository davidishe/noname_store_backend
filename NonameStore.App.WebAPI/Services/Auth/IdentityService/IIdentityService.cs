using System.Threading.Tasks;
using NonameStore.App.WebAPI.Models.Contracts;
using NonameStore.App.WebAPI.Models.Domain;

namespace NonameStore.App.WebAPI.Services
{
  public interface IIdentityService
  {

    Task<ExternalAuthResult> LoginWithFacebookAsync(string accessToken);
    Task<ExternalAuthResult> LoginWithGoogleAsync(string accessToken);

    Task<AccesssTokenResponse> GetFacebookAccessToken(string code);
    Task<AccesssTokenResponse> GetGoogleAccessToken(string code);

  }
}