using Microsoft.AspNetCore.Mvc;
using Musicians.Application.DTO;
using Musicians.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Musicians.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // POST api/<UsersController>
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
    {
        var user = await _userService.RegisterAsync(userRegistrationDto);
        if (user == null)
        {
            return BadRequest("Bad credentials, user already exists");
        }
        return Ok(user);
    }
    

}
