using System.Collections.Generic;

namespace NonameStore.App.WebAPI.Models.Contracts
{
  public class AuthFailedResponse
  {
    public IEnumerable<string> Errors { get; set; }
  }
}