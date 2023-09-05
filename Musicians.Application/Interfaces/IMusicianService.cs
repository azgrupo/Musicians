using Musicians.Application.DTO;
using Musicians.Domain.Entities;

namespace Musicians.Application.Interfaces;

public interface IMusicianService
{
    Task<Musician> CreateAsync(MusicianDto musicianDto);
    Task<IEnumerable<Musician>> GetAllAsync();
    Task<Musician> GetByIdAsync(int id);
    Task UpdateAsync(Musician musician);
    Task DeleteAsync(int id);
}
