using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI2.Models;

namespace MovieCatalogAPI2.Data
{
    // AppDbContext apraksta mūsu datubāzi
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Norādam kādas tabulas būs mūsu datubāzē (teacher style)
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Review> Reviews => Set<Review>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Genre -> Movie (One-To-Many)
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            // User -> Review (One-To-Many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Movie -> Review (One-To-Many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Movie)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            // Optional: configure decimal precision for Rating (if provider supports it)
            modelBuilder.Entity<Movie>()
                .Property(m => m.Rating)
                .HasPrecision(3, 1);

            // Seed example genres (optional, helpful for demo)
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Drama" },
                new Genre { Id = 3, Name = "Comedy" }
            );
        }
    }
}
