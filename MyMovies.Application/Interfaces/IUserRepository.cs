using MyMovies.Core.Entities;

namespace MyMovies.Application.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUsernameAsync(string username);
    Task<User> AddAsync(User user); 
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}
