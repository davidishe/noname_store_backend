using System.ComponentModel.DataAnnotations;

namespace NonameStore.App.WebAPI.Dtos
{
  public class UserToRegisterDto
  {
    [Required]
    public string DisplayName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Password must have number, uppercase, lowercase, non alphanumeric and six chars")]
    public string Password { get; set; }

  }
}