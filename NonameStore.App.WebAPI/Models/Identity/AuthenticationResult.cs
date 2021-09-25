using System.Collections.Generic;
using MyAppBack.Dtos;
using MyAppBack.Identity;

namespace MyAppBack.Domain
{
  public class ExternalAuthResult
  {
    public string? Token { get; set; }
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; }
    public UserToReturnDto? User { get; set; }
  }
}