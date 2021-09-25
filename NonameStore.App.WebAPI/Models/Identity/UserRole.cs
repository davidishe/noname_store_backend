using Microsoft.AspNetCore.Identity;
using MyAppBack.Identity;

namespace MyAppBack.Models.Identity
{
  public class UserRole : IdentityUserRole<int>
  {
    public virtual AppUser User { get; set; }
    public virtual Role Role { get; set; }

  }
}