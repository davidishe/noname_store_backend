using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MyAppBack.Models.Identity;

namespace MyAppBack.Identity
{
  public class AppUser : IdentityUser<int>
  {
    public string DisplayName { get; set; }
    public string? PictureUrl { get; set; }
    public virtual Address? Address { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }

  }
}