using System.Threading.Tasks;
using MyAppBack.Identity;

namespace MyAppBack.Services.TokenService
{
  public interface ITokenService
  {
    Task<string> CreateToken(AppUser user);
  }
}