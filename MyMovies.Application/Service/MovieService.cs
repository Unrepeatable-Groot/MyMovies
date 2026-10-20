using MyMovies.Application.Dtos;
using MyMovies.Application.Interfaces;
using MyMovies.Core.Entities;

namespace MyMovies.Application.Service;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }


    public async Task<MovieDto> CreateAsync(CreateOrUpdateMovieDto dto, int adminUserId)
    {
        var movie = new Movie
        {
            Title = dto.Title,
            CardImageUrl = dto.CardImageUrl,
            BannerImageUrl = dto.BannerImageUrl,
            Genre = dto.Genre,
            ReleaseYear = dto.ReleaseYear,
            Director = dto.Director,
            Actors = dto.Actors,
            Description = dto.Description,
            ImdbRating = dto.ImdbRating,
            Category = dto.Category,
            UploadedByUserId = adminUserId
        };

        await _movieRepository.AddAsync(movie);

        var created = await _movieRepository.GetByIdAsync(movie.Id);
        return new MovieDto(created!);
    }

    public async Task<List<MovieDto>> GetAllAsync()
    {
        var movie = await _movieRepository.GetAllAsync();   
        return movie.Select(m => new MovieDto(m)).ToList();
    }

    public async Task<List<MovieDto>> GetByCategoryAsync(MovieCategory category)
    {
        var movies = await _movieRepository.GetByCategoryAsync(category);
        return movies.Select(m => new MovieDto(m)).ToList();
    }

    public async Task<List<MovieDto>> GetByCountAsync(int count)
    {
        var movies = await _movieRepository.GetByCountAsync(count);
        return movies.Select(m => new MovieDto(m)).ToList();
    }

    public async Task<MovieDto?> GetByIdAsync(int id, int? currentUserId)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        return movie == null ? null : new MovieDto(movie, currentUserId);
    }

    public async Task<List<MovieDto>> GetByNameAsync(string name)
    {
        var movies = await _movieRepository.GetByNameAsync(name);
        return movies.Select(m => new MovieDto(m)).ToList();
    }

    public async Task<List<MovieDto>> GetPagedAsync(int page, int pageSize, MovieCategory category)
    {
        var movies = await _movieRepository.GetPagedAsync(page, pageSize, category);
        return movies.Select(m => new MovieDto(m)).ToList();
    }

    public async Task<List<MovieDto>> GetUserFavoritesAsync(int userId)
    {
        var movies = await _movieRepository.GetUserFavoritesAsync(userId);
        return movies.Select(m => new MovieDto(m)).ToList();
    }

    public async Task RemoveFromFavoritesAsync(int userId, int movieId)
    {
        var favorite = await _movieRepository.GetFavoriteAsync(userId, movieId)
        ?? throw new Exception("Movie is not in favorites.");

        await _movieRepository.RemoveFromFavoritesAsync(favorite);
    }

    public async Task AddOrUpdateReactionAsync(int userId, CreateOrUpdateReactionDto dto)
    {
        var movie = await _movieRepository.GetByIdAsync(dto.MovieId)
            ?? throw new Exception($"Movie with ID {dto.MovieId} does not exist.");

        var existingReaction = await _movieRepository.GetReactionAsync(userId, dto.MovieId);

        if (existingReaction == null)
        {
            var reaction = new Reaction
            {
                UserId = userId,
                MovieId = dto.MovieId,
                Type = dto.Type
            };

            await _movieRepository.AddReactionAsync(reaction);
        }
        else
        {
            existingReaction.Type = dto.Type;
            await _movieRepository.UpdateReactionAsync(existingReaction);
        }
    }

    public async Task AddToFavoritesAsync(int userId, int movieId)
    {
        var movie = await _movieRepository.GetByIdAsync(movieId)
        ?? throw new Exception($"Movie with ID {movieId} does not exist.");


        var count = await _movieRepository.GetUserFavoritesCountAsync(userId);
        if (count >= 50)
            throw new Exception("Favorites limit reached. Maximum 50 movies allowed.");


        var existingFavorite = await _movieRepository.GetFavoriteAsync(userId, movieId);
        if (existingFavorite != null)
            throw new Exception("Movie is already in favorites.");


        var favorite = new Favorite
        {
            UserId = userId,
            MovieId = movieId
        };
        await _movieRepository.AddToFavoritesAsync(favorite);
    }

    public async Task RemoveReactionAsync(int userId, int movieId)
    {
        var reaction = await _movieRepository.GetReactionAsync(userId, movieId)
            ?? throw new Exception("Reaction does not exist.");

        await _movieRepository.RemoveReactionAsync(reaction);
    }

    public async Task<List<MovieDto>> GetLatestByCategoryAsync(MovieCategory? category = null)
    {
        var movies = await _movieRepository.GetLatestByCategoryAsync(category);
        return movies.Select(m => new MovieDto(m)).ToList();
    }

    public async Task<List<MovieDto>> GetPopularByCategoryAsync(MovieCategory category)
    {
        var movies = await _movieRepository.GetPopularByCategoryAsync(category);
        return movies.Select(m => new MovieDto(m)).ToList();
    }

    public async Task<List<MovieDto>> GetRandomAsync(int? excludeId = null, MovieCategory? category = null)
    {
        var movies = await _movieRepository.GetRandomAsync(excludeId, category);
        return movies.Select(m => new MovieDto(m)).ToList();
    }

    public async Task<MovieDto> UpdateAsync(int id, CreateOrUpdateMovieDto dto)
    {
        var movie = await _movieRepository.GetByIdAsync(id)
            ?? throw new Exception($"Movie with ID {id} does not exist.");

        movie.Title = dto.Title;
        movie.CardImageUrl = dto.CardImageUrl;
        movie.BannerImageUrl = dto.BannerImageUrl;
        movie.Genre = dto.Genre;
        movie.ReleaseYear = dto.ReleaseYear;
        movie.Director = dto.Director;
        movie.Actors = dto.Actors;
        movie.Description = dto.Description;
        movie.ImdbRating = dto.ImdbRating;
        movie.Category = dto.Category;

        await _movieRepository.UpdateAsync(movie);
        return new MovieDto(movie);
    }

    public async Task DeleteAsync(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id)
            ?? throw new Exception($"Movie with ID {id} does not exist.");

        await _movieRepository.DeleteAsync(movie);
    }

    public async Task BulkCreateAsync(List<CreateOrUpdateMovieDto> dtos, int adminUserId)
    {
        foreach (var dto in dtos)
        {
            await CreateAsync(dto, adminUserId);
        }
    }
}
