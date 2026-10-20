using Microsoft.EntityFrameworkCore;
using MyMovies.Application.Interfaces;
using MyMovies.Core.Entities;

namespace MyMovies.Infrastructure.Persistence;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _context;

    public MovieRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Movie>> GetAllAsync()
    {
        return await _context.Movies
            .Include(m => m.Reactions)
            .Include(m => m.Favorites)
            .Include(m => m.UploadedByUser)
            .Include(m => m.Comments)
                .ThenInclude(c => c.User)
            .OrderByDescending(m => m.Id)
            .ToListAsync();
    }

    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await _context.Movies
            .Include(m => m.Reactions)
            .Include(m => m.Favorites)
            .Include(m => m.UploadedByUser)
            .Include(m => m.Comments)
                .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Movie>> GetByCategoryAsync(MovieCategory category)
    {
        return await _context.Movies
            .Where(m => m.Category == category)
            .OrderByDescending(m => m.Id)
            .ToListAsync();
    }

    public async Task<List<Movie>> GetByNameAsync(string name)
    {
        return await _context.Movies
            .Where(m => m.Title.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }

    public async Task<List<Movie>> GetByCountAsync(int count)
    {
        return await _context.Movies
            .Take(count)
            .ToListAsync();
    }

    public async Task<List<Movie>> GetPagedAsync(int page, int pageSize, MovieCategory category)
    {
        return await _context.Movies
            .Where(m => m.Category == category)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Movie> AddAsync(Movie movie)
    {
        await _context.Movies.AddAsync(movie);
        await _context.SaveChangesAsync();
        return movie;
    }

    public async Task UpdateAsync(Movie movie)
    {
        _context.Movies.Update(movie);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Movie movie)
    {
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Movie>> GetUserFavoritesAsync(int userId)
    {
        return await _context.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.Movie)
                .ThenInclude(m => m!.Reactions)
            .Include(f => f.Movie)
                .ThenInclude(m => m!.UploadedByUser)
            .Select(f => f.Movie!)
            .ToListAsync();
    }

    public async Task AddToFavoritesAsync(Favorite favorite)
    {
        await _context.Favorites.AddAsync(favorite);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromFavoritesAsync(Favorite favorite)
    {
        _context.Favorites.Remove(favorite);
        await _context.SaveChangesAsync();
    }

    public async Task<Favorite?> GetFavoriteAsync(int userId, int movieId)
    {
        return await _context.Favorites
            .FirstOrDefaultAsync(f => f.UserId == userId && f.MovieId == movieId);
    }

    public async Task<int> GetUserFavoritesCountAsync(int userId)
    {
        return await _context.Favorites
            .CountAsync(f => f.UserId == userId);
    }

    public async Task<Reaction?> GetReactionAsync(int userId, int movieId)
    {
        return await _context.Reactions
            .FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == movieId);
    }

    public async Task AddReactionAsync(Reaction reaction)
    {
        await _context.Reactions.AddAsync(reaction);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Movie>> GetLatestByCategoryAsync(MovieCategory? category = null)
    {
        return await _context.Movies
            .Where(m => category == null || m.Category == category)
            .OrderByDescending(m => m.Id)
            .Take(12)
            .ToListAsync();
    }

    public async Task<List<Movie>> GetPopularByCategoryAsync(MovieCategory category)
    {
        return await _context.Movies
            .Where(m => m.Category == category)
            .Include(m => m.Reactions)
            .OrderByDescending(m => m.Reactions.Count(r => r.Type == ReactionType.Like))
            .Take(6)
            .ToListAsync();
    }

    public async Task<List<Movie>> GetRandomAsync(int? excludeId = null, MovieCategory? category = null)
    {
        return await _context.Movies
            .Where(m => excludeId == null || m.Id != excludeId)
            .Where(m => category == null || m.Category == category)
            .OrderBy(m => Guid.NewGuid())
            .Take(6)
            .ToListAsync();
    }

    public async Task UpdateReactionAsync(Reaction reaction)
    {
        _context.Reactions.Update(reaction);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveReactionAsync(Reaction reaction)
    {
        _context.Reactions.Remove(reaction);
        await _context.SaveChangesAsync();
    }
}
