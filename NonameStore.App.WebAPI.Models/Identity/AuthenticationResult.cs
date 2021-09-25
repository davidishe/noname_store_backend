using System.Collections.Generic;
using NonameStore.App.WebAPI.Models.Dtos;

namespace NonameStore.App.WebAPI.Models.Domain
{
  public class ExternalAuthResult
  {
    public string? Token { get; set; }
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; }
    public UserToReturnDto? User { get; set; }
  }
}