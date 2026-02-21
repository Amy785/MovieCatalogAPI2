using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI2.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        // Store hashed password only
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
