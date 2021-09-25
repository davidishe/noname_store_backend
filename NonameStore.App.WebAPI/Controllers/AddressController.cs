using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dadata;
using Dadata.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyAppBack.Controllers
{

  [AllowAnonymous]
  public class AddressController : BaseApiController
  {

    private readonly string token = "5f6e87441948ba1d00eb29d7cf605873cd17c722";


    [Route("suggest2")]
    [HttpGet]
    public async Task<ActionResult<object>> GetOrdersForUserFull(string address)
    {
      var api = new SuggestClientAsync(token);
      var response = await api.SuggestAddress(address);
      var data = response.suggestions;
      return Ok(data);
    }

    [Route("suggest")]
    [HttpGet]
    public async Task<ActionResult<object>> GetOrdersForUser(string address)
    {
      var api = new SuggestClientAsync(token);
      var response = await api.SuggestAddress(address);
      var data = response.suggestions;
      List<string> dataToReturn = new List<string>();

      foreach (var item in data)
      {
        dataToReturn.Add(item.value);
      }

      return Ok(dataToReturn);
    }

  }
}