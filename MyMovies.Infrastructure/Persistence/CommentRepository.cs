using Microsoft.EntityFrameworkCore;
using MyMovies.Application.Interfaces;
using MyMovies.Core.Entities;

namespace MyMovies.Infrastructure.Persistence;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return await _context.Comments
            .Include(c => c.User)
            .FirstAsync(c => c.Id == comment.Id);
    }

    public async Task DeleteAsync(Comment comment)
    {
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments
        .Include(c => c.User)
        .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Comment>> GetByMovieIdAsync(int movieId)
    {
        return await _context.Comments
        .Where(c => c.MovieId == movieId)
        .Include(c => c.User)
        .OrderByDescending(m => m.Id)
        .ToListAsync();
    }

    public async Task UpdateAsync(Comment comment)
    {
        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
    }
}
