using Musicians.Domain.Entities;

namespace Musicians.Application.Interfaces;

public interface IMusicianRepository
{
    Task<Musician> CreateAsync(Musician musician);
    Task<IEnumerable<Musician>> GetAllAsync();
    Task<Musician> GetByIdAsync(int id);
    Task UpdateAsync(Musician musician);
    Task DeleteAsync(int id);
}
