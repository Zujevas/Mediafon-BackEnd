using Back_end.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var user = new IdentityUser
            {
                UserName = registerModel.Username,
                PasswordHash = registerModel.Password
            };

            var result = await _userManager.CreateAsync(user, user.PasswordHash!);
            if (result.Succeeded)
            {
                return Ok(new { isSuccessful = true, message = "Registration successful" });
            }

            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { Errors = errors });
        }

    }
}
