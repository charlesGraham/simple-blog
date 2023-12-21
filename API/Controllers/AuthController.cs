using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager)
    {
      this._userManager = userManager;
    }


    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequestDto request)
    {
      // IdentityUser object
      var user = new IdentityUser
      {
        UserName = request.Email?.Trim(),
        Email = request.Email?.Trim()
      };

      // create user
      var identityResult = await _userManager.CreateAsync(user, request.Password);

      if (identityResult.Succeeded)
      {
        // add Reader role to user 
        identityResult = await _userManager.AddToRoleAsync(user, "Reader");

        if (identityResult.Succeeded)
        {
          return Ok();
        }
        else
        {
          if (identityResult.Errors.Any())
          {
            foreach (var error in identityResult.Errors)
            {
              ModelState.AddModelError("", error.Description);
            }
          }
        }
      }
      else
      {
        if (identityResult.Errors.Any())
        {
          foreach (var error in identityResult.Errors)
          {
            ModelState.AddModelError("", error.Description);
          }
        }
      }

      return ValidationProblem(ModelState);
    }
  }
}