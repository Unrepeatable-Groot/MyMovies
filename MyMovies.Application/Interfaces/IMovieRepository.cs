using MyMovies.Core.Entities;

namespace MyMovies.Application.Interfaces;

public interface IMovieRepository
{
    Task<List<Movie>> GetAllAsync();
    Task<List<Movie>> GetByNameAsync(string name);
    Task<Movie?> GetByIdAsync(int id);
    Task<List<Movie>> GetByCategoryAsync(MovieCategory category);
    Task<List<Movie>> GetByCountAsync(int count);
    Task<List<Movie>> GetPagedAsync(int page, int pageSize, MovieCategory category);
    Task<Movie> AddAsync(Movie movie);
    Task UpdateAsync(Movie movie);
    Task DeleteAsync(Movie movie);
    Task<List<Movie>> GetUserFavoritesAsync(int userId);
    Task AddToFavoritesAsync(Favorite favorite);
    Task RemoveFromFavoritesAsync(Favorite favorite);
    Task<Favorite?> GetFavoriteAsync(int userId, int movieId);
    Task<int> GetUserFavoritesCountAsync(int userId);
    Task<Reaction?> GetReactionAsync(int userId, int movieId);
    Task AddReactionAsync(Reaction reaction);
    Task UpdateReactionAsync(Reaction reaction);
    Task RemoveReactionAsync(Reaction reaction);
    Task<List<Movie>> GetLatestByCategoryAsync(MovieCategory? category = null);
    Task<List<Movie>> GetPopularByCategoryAsync(MovieCategory category);
    Task<List<Movie>> GetRandomAsync(int? excludeId = null, MovieCategory? category = null);
}
