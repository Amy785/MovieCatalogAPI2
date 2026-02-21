using MovieCatalogAPI2.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI2.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign keys
        [Required]
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        [Required]
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
