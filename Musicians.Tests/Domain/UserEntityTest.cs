using Musicians.Domain.Entities;
using FluentValidation.TestHelper;
using Musicians.Domain.Validation;

namespace Musicians.Tests.Domain;

public class UserEntityTest
{
    private readonly UserValidator _userValidator;
    public UserEntityTest()
    {
        _userValidator = new UserValidator();
    }

    [Fact]
    public void User_With_Empty_Values_Fails()
    {
        // Arrange
        User user = new User();

        // Act
        var response = _userValidator.TestValidate(user);

        // Assert
        response.ShouldHaveValidationErrorFor(u => u.Email);
        response.ShouldHaveValidationErrorFor(u => u.Password);
    }

    [Fact]
    public void User_With_Valid_Values_Pass()
    {
        // Arrange
        User user = new User
        {
            Id = 1,
            Email = "auseremail@musicians.com",
            Password = "password"
        };

        // Act
        var response = _userValidator.TestValidate(user);

        // Assert
        response.ShouldNotHaveValidationErrorFor(u => u.Email);
        response.ShouldNotHaveValidationErrorFor(u => u.Password);
    }

    [Fact]
    public void User_With_Invalid_Email_Fails()
    {
        // Arrange
        User user = new User
        {
            Id = 1,
            Email = "auseremail",
            Password = "password"
        };

        // Act
        var response = _userValidator.TestValidate(user);

        // Assert
        response.ShouldHaveValidationErrorFor(u => u.Email).WithErrorMessage("Invalid Email");
        response.ShouldNotHaveValidationErrorFor(u => u.Password);
    }

    [Fact]
    public void User_With_Invalid_Password_Fails()
    {
        // Arrange
        User user = new User
        {
            Id = 1,
            Email = "auseremail@musicians.com",
            Password = "pass"
        };

        // Act
        var response = _userValidator.TestValidate(user);

        // Assert
        response.ShouldNotHaveValidationErrorFor(u => u.Email);
        response.ShouldHaveValidationErrorFor(u => u.Password).WithErrorMessage("Password too short. Minimun is 8 characters");
    }

}
