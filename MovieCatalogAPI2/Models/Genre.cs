using MovieCatalogAPI2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI2.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}

