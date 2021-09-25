using System.ComponentModel.DataAnnotations;

namespace NonameStore.App.WebAPI.Dtos
{
  public class UserToLoginDto
  {
    public string Email { get; set; }
    public string Password { get; set; }
  }
}