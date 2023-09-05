using FluentValidation;
using Musicians.Application.DTO;
using Musicians.Application.Interfaces;
using Musicians.Domain.Entities;

namespace Musicians.Application.Service
{
    public class MusicianService : IMusicianService
    {
        private readonly IMusicianRepository _musicianRepository;
        private readonly IValidator<Musician> _validator;

        public MusicianService(IMusicianRepository musicianRepository, IValidator<Musician> validator)
        {
            _musicianRepository = musicianRepository;
            _validator = validator;
        }
        public async Task<Musician> CreateAsync(MusicianDto musicianDto)
        {
            Musician musician = new Musician
            {
                Name = musicianDto.Name,
                Genre = musicianDto.Genre,
                PerformAs = musicianDto.PerformAs,
                IntroDate = musicianDto.IntroDate,
                Instrument = musicianDto.Instrument                
            };

            var checkValidation = _validator.Validate(musician);

            if(!checkValidation.IsValid) 
            {
                throw new ValidationException(checkValidation.Errors);
            }

            return await _musicianRepository.CreateAsync(musician);
        }

        public async Task DeleteAsync(int id)
        {
            var musician = await GetByIdAsync(id);
            if (musician == null)
            {
                throw new ArgumentException("Musician not found");
            }

            await _musicianRepository.DeleteAsync(musician.Id);
        }

        public async Task<IEnumerable<Musician>> GetAllAsync()
        {
            return await _musicianRepository.GetAllAsync();
        }

        public async Task<Musician> GetByIdAsync(int id)
        {
            return await _musicianRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Musician musician)
        {
            var musicianFound = await GetByIdAsync(musician.Id);

            if (musicianFound == null)
            {
                throw new ArgumentException("Musician not found.");
            }

            musicianFound.Name = musician.Name;
            musicianFound.Genre = musician.Genre;
            musicianFound.PerformAs = musician.PerformAs;
            musicianFound.IntroDate = musician.IntroDate;
            musicianFound.Instrument = musician.Instrument;
            await _musicianRepository.UpdateAsync(musicianFound);
        }
    }
}
