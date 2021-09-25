using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace NonameStore.App.WebAPI.Models.Identity
{
  public class Role : IdentityRole<int>
  {
    public virtual ICollection<UserRole> UserRoles { get; set; }
  }
}