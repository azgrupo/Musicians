using FluentValidation;
using Microsoft.Extensions.Configuration;
using Moq;
using Musicians.Application.DTO;
using Musicians.Application.Interfaces;
using Musicians.Application.Service;
using Musicians.Domain.Entities;
using Musicians.Domain.Validation;

namespace Musicians.Tests.Application;

public  class UserServiceTest
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IConfiguration> _configuration;
    private readonly UserValidator _userValidator;

    public UserServiceTest()
    {
        _userRepository = new Mock<IUserRepository>();
        _configuration = new Mock<IConfiguration>();
        _userValidator = new UserValidator();
        _userService = new UserService(_userRepository.Object, _userValidator, _configuration.Object);
    }

    [Fact]
    public async Task UserService_ShouldCreateUser()
    {
        // Arrange
        var userRegistration = new UserRegistrationDto
        {            
            Email = "anewuser@musicians.com",
            Password = "password123"
        };

        _userRepository
            .Setup(x => x.GetUserByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null);

        _userRepository
            .Setup(x => x.AddAsync(It.IsAny<User>()))
            .ReturnsAsync(new User
            {
                Id = 1,
                Email = userRegistration.Email,
                Password = userRegistration.Password
            });

        // Act
        var result = await _userService.RegisterAsync(userRegistration);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userRegistration.Email, result.Email);
        Assert.Equal(userRegistration.Password, result.Password);
        _userRepository.Verify(x => x.GetUserByEmailAsync(userRegistration.Email), Times.Once);
        _userRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task UserService_Should_Throw_Exception_if_user_exists()
    {
        // Arrange
        var user = new UserRegistrationDto
        {
            Email = "anewuser@musicians.com",
            Password = "password123"
        };

        _userRepository
            .Setup(x => x.GetUserByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new User { Id = 1 });

        // Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await _userService.RegisterAsync(user));
        _userRepository.Verify(x => x.GetUserByEmailAsync(user.Email), Times.Once);
        _userRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Never);
    }



}
