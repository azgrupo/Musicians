using Musicians.Domain.Entities;
using FluentValidation;
using Musicians.Domain.Validation;
using Musicians.Application.Interfaces;
using Musicians.Application.Service;
using Moq;
using Musicians.Application.DTO;

namespace Musicians.Tests.Application;

public class MusicianServiceTest
{
    private readonly MusicianValidator _musicianValidator;
    private readonly IMusicianService _musicianService;
    private readonly Mock<IMusicianRepository> _musicianRepository;
    public MusicianServiceTest()
    {
        _musicianValidator = new MusicianValidator();
        _musicianRepository = new Mock<IMusicianRepository>(); 
        _musicianService = new MusicianService(_musicianRepository.Object, _musicianValidator);
    }

    [Fact]
    public async Task MusicianService_Should_Create_RecordAsync()
    {
        // Arrange
        var musicianDto = new MusicianDto { Name = "Test", Genre="Rock", PerformAs="Band",IntroDate=DateTime.UtcNow, Instrument = ""};
        var musician = new Musician { Id = 1, Name = "Test", Genre = "Rock", PerformAs = "Band", IntroDate = DateTime.UtcNow, Instrument = "" };            

        _musicianRepository
            .Setup(x => x.CreateAsync(It.IsAny<Musician>()))
            .ReturnsAsync(new Musician { Id = 1, Name = "Test", Genre = "Rock", PerformAs = "Band", IntroDate = DateTime.UtcNow, Instrument = "" });


        // Act
        var result = await _musicianService.CreateAsync(musicianDto);

        // Assert
        _musicianRepository.Verify(x => x.CreateAsync(It.IsAny<Musician>()), Times.Once());
        Assert.Equal(musician.Id, result.Id);
        Assert.Equal(musician.Name, result.Name);
        Assert.Equal(musician.Genre, result.Genre);
        Assert.Equal(musician.PerformAs, result.PerformAs);
        Assert.InRange(musician.IntroDate, result.IntroDate.AddSeconds(-1), result.IntroDate.AddSeconds(1));
        Assert.Equal(musician.Instrument, result.Instrument);
    }

    [Fact]
    public async Task MusicianService_ShouldReturnExistingRecord()
    {
        // Arrange
        int musicianId = 1;
        _musicianRepository
            .Setup(x => x.GetByIdAsync(musicianId))
            .ReturnsAsync(new Musician { Id = musicianId, Name = "Test", Genre = "Rock", PerformAs = "Band", IntroDate = DateTime.UtcNow, Instrument = "" });

        // Act
        var result = await _musicianService.GetByIdAsync(musicianId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(musicianId, result.Id);
        _musicianRepository.Verify(x => x.GetByIdAsync(musicianId), Times.Once);
    }

    [Fact]
    public async Task MusicianService_ShouldReturnAllRecords()
    {
        // Arrange
        var musicians = new List<Musician>
        {
            new Musician { Id = 1, Name = "Test", Genre = "Rock", PerformAs = "Band", IntroDate = DateTime.UtcNow, Instrument = "" },
            new Musician { Id = 2, Name = "Rush", Genre = "Progressive Rock", PerformAs = "Band", IntroDate = DateTime.UtcNow, Instrument = "" }
        };
        _musicianRepository
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(musicians);

        // Act
        var result = await _musicianService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(musicians.Count, result.Count());
        _musicianRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task MusicService_ShouldUpdateRecord()
    {
        // Arrange
        var musician = new Musician { Id = 1, Name = "Rush", Genre = "Rock", PerformAs = "Band", IntroDate = DateTime.UtcNow, Instrument = "" };
        _musicianRepository
            .Setup(x => x.GetByIdAsync(musician.Id))
            .ReturnsAsync(musician);

        // Act
        var modifiedRecord = new Musician { Id = 1, Name = "Rush", Genre = "Progressive Rock", PerformAs = "Band", IntroDate = DateTime.UtcNow, Instrument = "" };
        await _musicianService.UpdateAsync(modifiedRecord);

        // Assert
        _musicianRepository.Verify(x => x.GetByIdAsync(musician.Id), Times.Once);
        _musicianRepository.Verify(x => x.UpdateAsync(It.Is<Musician>(m => m.Name == modifiedRecord.Name && 
            m.Genre == modifiedRecord.Genre)), Times.Once);
    }

    [Fact]
    public async Task MusicianService_Should_Throw_ValidationException_When_Name_Is_Empty()
    {
        // Arrange
        var musicianDto = new MusicianDto { Name = "", Genre = "Progressive Rock", PerformAs = "Band", IntroDate = DateTime.UtcNow, Instrument = "" };

        // Assert
        await Assert.ThrowsAsync<ValidationException>(() => _musicianService.CreateAsync(musicianDto));
    }

    [Fact]
    public async Task MusicianService_Should_Throw_ValidationException_When_Genre_Is_Empty()
    {
        // Arrange
        var musicianDto = new MusicianDto { Name = "Rush", Genre = "", PerformAs = "Band", IntroDate = DateTime.UtcNow, Instrument = "" };

        // Assert
        await Assert.ThrowsAsync<ValidationException>(() => _musicianService.CreateAsync(musicianDto));
    }

    [Fact]
    public async Task MusicianService_Should_Throw_ValidationException_When_PerformAs_Is_Empty()
    {
        // Arrange
        var musicianDto = new MusicianDto { Name = "Rush", Genre = "Progressive Rock", PerformAs = "", IntroDate = DateTime.UtcNow, Instrument = "" };

        // Assert
        await Assert.ThrowsAsync<ValidationException>(() => _musicianService.CreateAsync(musicianDto));
    }

}