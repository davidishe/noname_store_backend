using System.Threading.Tasks;
using NonameStore.App.WebAPI.Identity;
using NonameStore.App.WebAPI.Models.Identity;

namespace NonameStore.App.WebAPI.Services.TokenService
{
  public interface ITokenService
  {
    Task<string> CreateToken(AppUser user);
  }
}