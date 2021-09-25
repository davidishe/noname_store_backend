using Microsoft.AspNetCore.Mvc;
using NonameStore.App.WebAPI.Errors;

namespace NonameStore.App.WebAPI.Controllers
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