using Musicians.Application.DTO;
using Musicians.Domain.Entities;

namespace Musicians.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserTokenDto?> LoginAsync(UserLoginDto userDto);
        Task<UserRegistrationDto?> RegisterAsync(UserRegistrationDto userDto);
        Task<User> GetUserByEmailAsync(string email);
    }
}
