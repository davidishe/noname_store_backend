using System.Collections.Generic;
using MyAppBack.Models.Identity;

namespace MyAppBack.Dtos
{
  public class UserToReturnDto
  {
    public string Email { get; set; }
    public string DisplayName { get; set; }
    public AddressDto? Address { get; set; }
    public string Token { get; set; }
    public IList<string> Roles { get; set; }

  }
}