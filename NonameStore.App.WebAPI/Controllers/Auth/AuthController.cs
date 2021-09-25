using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyAppBack.Identity;
using Newtonsoft.Json;

namespace MyAppBack.Controllers
{

  [AllowAnonymous]
  public class AuthController : BaseApiController
  {
    // private readonly UserManager<AppUser> _userManager;
    // private readonly SignInManager<AppUser> _signInManager;

    // public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    // {
    //   _signInManager = signInManager;
    //   _userManager = userManager;
    // }

    // [HttpPost]
    // [Route("login")]
    // [AllowAnonymous]
    // public string ExternalLogin(string? provider = "Facebook", string? returnUrl = "www.ya.ru")
    // {
    //   var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);
    //   //   var callBackUrl = "www.yandex.ru";
    //   //   properties.RedirectUri = callBackUrl;
    //   //   return Challenge(properties, provider);
    //   var x = Challenge(properties, provider);
    //   return JsonConvert.SerializeObject(x);

    // }


    // // [HttpPost]
    // // [Route("facebook")]
    // // [AllowAnonymous]
    // // public string ExternalLoginFacebook(string? provider = "Facebook", string? returnUrl = "www.ya.ru", bool generateState? = false)
    // // {
    // //   IEnumerable<AuthenticationDescription> descriptions

    // // }

    // [HttpGet]
    // [Route("providers")]
    // public async Task<IActionResult> GetProviders()
    // {
    //   var providers = await _signInManager.GetExternalAuthenticationSchemesAsync();
    //   return Ok(providers);
    // }


  }
}