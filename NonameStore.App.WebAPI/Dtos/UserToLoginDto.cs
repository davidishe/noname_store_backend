using System.ComponentModel.DataAnnotations;

namespace MyAppBack.Dtos
{
  public class UserToLoginDto
  {
    public string Email { get; set; }
    public string Password { get; set; }
  }
}