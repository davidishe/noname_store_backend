using System.ComponentModel.DataAnnotations;
namespace MyAppBack.Models.Identity
{
  public class UserRegistrationRequests
  {
    public class UserRegistrationRequest
    {
      [EmailAddress]
      public string Email { get; set; }

      public string Password { get; set; }
    }
  }
}