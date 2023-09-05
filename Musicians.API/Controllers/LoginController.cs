using Microsoft.AspNetCore.Mvc;
using Musicians.Application.DTO;
using Musicians.Application.Interfaces;


namespace Musicians.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;

    public LoginController(IUserService userService)
    {
        _userService = userService;
    }
    // POST api/<UsersController>
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var user = await _userService.LoginAsync(userLoginDto);

        if (user.JwtToken != null)
        {
            return Ok(user);
        }

        return Unauthorized("Invalid credentials");
    }
}
