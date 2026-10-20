using MyMovies.Application.Dtos;
using MyMovies.Application.Interfaces;
using MyMovies.Core.Entities;

namespace MyMovies.Application.Service;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }


    public async Task<CommentDto> CreateAsync(CreateOrUpdateCommentDto dto, int userId)
    {
        var comment = new Comment
        {
            Content = dto.Content,
            MovieId = dto.MovieId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };
        var created = await _commentRepository.AddAsync(comment);
        return new CommentDto(created);
    }

    public async Task<List<CommentDto>> GetByMovieIdAsync(int movieId)
    {
        var comments = await _commentRepository.GetByMovieIdAsync(movieId);
        return comments.Select(c => new CommentDto(c)).ToList();
    }

    public async Task<CommentDto> UpdateAsync(int id, CreateOrUpdateCommentDto dto, int userId)
    {
        var comment = await _commentRepository.GetByIdAsync(id)
            ?? throw new Exception($"Comment with ID {id} does not exist.");

        if (comment.UserId != userId)
            throw new Exception("You are not allowed to edit this comment.");

        comment.Content = dto.Content;

        await _commentRepository.UpdateAsync(comment);
        return new CommentDto(comment);
    }

    public async Task DeleteAsync(int id, int userId, UserRole userRole)
    {
        var comment = await _commentRepository.GetByIdAsync(id)
        ?? throw new Exception($"Comment with ID {id} does not exist.");

        if (comment.UserId != userId && userRole != UserRole.Admin)
            throw new Exception("You are not allowed to delete this comment.");

        await _commentRepository.DeleteAsync(comment);
    }
}
