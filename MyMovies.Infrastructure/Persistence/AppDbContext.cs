using Microsoft.EntityFrameworkCore;
using MyMovies.Core.Entities;

namespace MyMovies.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Reaction> Reactions { get; set; }
    public DbSet<Favorite> Favorites { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureUsers(modelBuilder);
        ConfigureMovies(modelBuilder);
        ConfigureComments(modelBuilder);
        ConfigureReactions(modelBuilder);
        ConfigureFavorites(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void ConfigureUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(64);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(u => u.Role)
                .IsRequired()
                .HasDefaultValue(UserRole.User);


            entity.HasIndex(u => u.Email)
                .IsUnique();

            entity.HasIndex(u => u.Username)
                .IsUnique();


            entity.HasMany(u => u.UploadedMovies)
                .WithOne(m => m.UploadedByUser)
                .HasForeignKey(m => m.UploadedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(u => u.Comments)
                 .WithOne(c => c.User)
                 .HasForeignKey(c => c.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
        });
    }
    private void ConfigureMovies(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(256);

            entity.Property(m => m.CardImageUrl)
                .IsRequired();

            entity.Property(m => m.BannerImageUrl)
                .IsRequired();

            entity.Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(128);

            entity.Property(m => m.Director)
                .IsRequired()
                .HasMaxLength(128);

            entity.Property(m => m.Actors)
                .IsRequired();

            entity.Property(m => m.Description)
                .IsRequired();

            entity.Property(m => m.ImdbRating)
                .IsRequired();

            entity.Property(m => m.Category)
                .IsRequired();

            entity.Property(m => m.ReleaseYear)
                .IsRequired();


            entity.HasMany(m => m.Comments)
                .WithOne(c => c.Movie)
                .HasForeignKey(c => c.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(m => m.Reactions)
                .WithOne(r => r.Movie)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(m => m.Favorites)
                .WithOne(f => f.Movie)
                .HasForeignKey(f => f.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private void ConfigureComments(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(c => c.Content)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(c => c.CreatedAt)
                .IsRequired();
        });
    }

    private void ConfigureReactions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reaction>(entity =>
        {
            entity.Property(r => r.Type)
                .IsRequired();

            entity.HasIndex(r => new { r.UserId, r.MovieId })
                .IsUnique();


            entity.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private void ConfigureFavorites(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasIndex(f => new { f.UserId, f.MovieId })
                .IsUnique();


            entity.HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
