using Microsoft.AspNetCore.Mvc;
using MyAppBack.Errors;

namespace MyAppBack.Controllers
{
  [Route("errors/{code}")]
  public class ErrorController : BaseApiController
  {
    public IActionResult Error(int code)
    {
      return new ObjectResult(new ApiResponse(code));
    }
  }
}