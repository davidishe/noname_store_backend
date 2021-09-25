namespace NonameStore.App.WebAPI.Models.Dtos.Identity
{
  public class RefreshTokenRequest
  {
    public string Token { get; set; }

    public string RefreshToken { get; set; }
  }
}