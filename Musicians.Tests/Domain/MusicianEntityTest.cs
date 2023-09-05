using Musicians.Domain.Entities;
using FluentValidation.TestHelper;
using Musicians.Domain.Validation;

namespace Musicians.Tests.Domain
{
    public class MusicianEntityTest
    {
        private readonly MusicianValidator _musicianValidator;
        public MusicianEntityTest()
        {
            _musicianValidator = new MusicianValidator();
        }

        [Fact]
        public void Musician_With_Empty_Values_Fails()
        {
            // Arrange
            Musician musician = new Musician();

            // Act
            var response = _musicianValidator.TestValidate(musician);

            Console.Write(response.ToString());

            // Assert
            response.ShouldHaveValidationErrorFor(m => m.Name);
            response.ShouldHaveValidationErrorFor(m => m.Genre);
            response.ShouldHaveValidationErrorFor(m => m.PerformAs);       
        }

        [Fact]
        public void Musician_With_Valid_Values_Pass()
        {
            // Arrange
            Musician musician = new Musician
            {
                Id = 8,
                Name = "Rush",
                Genre = "Progressive Rock",
                PerformAs = "Band",
                IntroDate = DateTime.Now,
                Instrument = null
            };

            // Act
            var response = _musicianValidator.TestValidate(musician);

            // Assert
            response.ShouldNotHaveValidationErrorFor(m => m.Name);
            response.ShouldNotHaveValidationErrorFor(m => m.Genre);
            response.ShouldNotHaveValidationErrorFor(m => m.PerformAs);
        }
    }
}