using MyMovies.Application.Dtos;
using MyMovies.Core.Entities;

namespace MyMovies.Application.Interfaces;

public interface IMovieService
{
    Task<List<MovieDto>> GetAllAsync();
    Task<List<MovieDto>> GetByNameAsync(string name);
    Task<MovieDto?> GetByIdAsync(int id, int? currentUserId);
    Task<List<MovieDto>> GetByCategoryAsync(MovieCategory category);
    Task<List<MovieDto>> GetByCountAsync(int count);
    Task<List<MovieDto>> GetPagedAsync(int page, int pageSize, MovieCategory category);
    Task<MovieDto> CreateAsync(CreateOrUpdateMovieDto dto, int adminUserId);
    Task BulkCreateAsync(List<CreateOrUpdateMovieDto> dtos, int adminUserId);
    Task<MovieDto> UpdateAsync(int id, CreateOrUpdateMovieDto dto);
    Task DeleteAsync(int id);
    Task AddToFavoritesAsync(int userId, int movieId);
    Task RemoveFromFavoritesAsync(int userId, int movieId);
    Task<List<MovieDto>> GetUserFavoritesAsync(int userId);
    Task AddOrUpdateReactionAsync(int userId, CreateOrUpdateReactionDto dto);
    Task RemoveReactionAsync(int userId, int movieId);
    Task<List<MovieDto>> GetLatestByCategoryAsync(MovieCategory? category = null);
    Task<List<MovieDto>> GetPopularByCategoryAsync(MovieCategory category);
    Task<List<MovieDto>> GetRandomAsync(int? excludeId = null, MovieCategory? category = null);
}
