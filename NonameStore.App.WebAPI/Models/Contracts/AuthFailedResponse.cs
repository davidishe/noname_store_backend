using System.Collections.Generic;

namespace MyAppBack.Models.Contracts
{
  public class AuthFailedResponse
  {
    public IEnumerable<string> Errors { get; set; }
  }
}