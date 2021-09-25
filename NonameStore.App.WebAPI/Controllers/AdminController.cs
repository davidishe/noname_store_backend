using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NonameStore.App.WebAPI.Models.Dtos;
using NonameStore.App.WebAPI.Models.Identity;

namespace NonameStore.App.WebAPI.Controllers
{

  [Authorize]
  public class AdminController : BaseApiController
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    // 
    public AdminController(UserManager<AppUser> userManager, IMapper mapper)
    {
      _mapper = mapper;
      _userManager = userManager;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("create")]
    public async Task<ActionResult<List<UserToReturnDto>>> GetAllUsers()
    {
      var users = await _userManager.Users.Include(x => x.Address).ToListAsync();
      var usersToReturn = _mapper.Map<List<AppUser>, List<UserToReturnDto>>(users);

      if (usersToReturn != null) return Ok(usersToReturn);
      return BadRequest("Не удалось получить список пользователей");
    }

  }

}