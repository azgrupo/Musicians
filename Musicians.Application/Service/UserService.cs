using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Musicians.Application.DTO;
using Musicians.Application.Interfaces;
using Musicians.Domain.Entities;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Musicians.Application.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _userValidator;
    private readonly IConfiguration _configuration;
    private object object1;
    private IValidator<User> userValidator;
    private object object2;

    public UserService(IUserRepository userRepository, IValidator<User> validator, IConfiguration config)
    {
        _userRepository = userRepository;
        _userValidator = validator;
        _configuration = config;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
         return await _userRepository.GetUserByEmailAsync(email);
    }

    public async Task<UserTokenDto?> LoginAsync(UserLoginDto userDto)
    {
        User loggedUser = await GetUserByEmailAsync(userDto.Email);
        
        if (loggedUser != null)
        {
            // validate user match
            if ((loggedUser.Email == userDto.Email) &&
               (BCrypt.Net.BCrypt.Verify(userDto.Password, loggedUser.Password)))
            {
                //return with token
                return new UserTokenDto
                {
                    Id = loggedUser.Id,
                    Email = loggedUser.Email,
                    JwtToken = CreateToken(loggedUser)
                };

            }
        } 
        
        // return null token
        return null;
    }

    public async Task<UserRegistrationDto?> RegisterAsync(UserRegistrationDto userDto)
    {
        // check if user already exists
        if (await GetUserByEmailAsync(userDto.Email) != null)
        {
            return null;            
        }

        // User doesn't exists, add user
        User user = new User
        {
            Email = userDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password)  //quick password hash
        };

        var registeredUser = await _userRepository.AddAsync(user);

        return new UserRegistrationDto
        {
            Email = registeredUser.Email,
            Password = userDto.Password
        };
    }

    private string CreateToken(User loggedUser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JwtKey"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddDays(1),
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, loggedUser.Id.ToString()) }),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }
}
