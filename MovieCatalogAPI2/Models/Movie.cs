using MovieCatalogAPI2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCatalogAPI2.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(1888, 2100)]
        public int Year { get; set; }

        // Rating as decimal (0.0 - 10.0) optional
        [Column(TypeName = "decimal(3,1)")]
        public decimal? Rating { get; set; }

        // Foreign key to Genre
        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
