using MyMovies.Application.Dtos;
using MyMovies.Core.Entities;

namespace MyMovies.Application.Interfaces;

public interface ICommentService
{
    Task<List<CommentDto>> GetByMovieIdAsync(int movieId);
    Task<CommentDto> CreateAsync(CreateOrUpdateCommentDto dto, int userId);
    Task<CommentDto> UpdateAsync(int id, CreateOrUpdateCommentDto dto, int userId);
    Task DeleteAsync(int id, int userId, UserRole userRole);
}
