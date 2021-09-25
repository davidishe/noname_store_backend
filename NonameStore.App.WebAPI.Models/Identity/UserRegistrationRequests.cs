using System.ComponentModel.DataAnnotations;
namespace NonameStore.App.WebAPI.Models.Identity
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