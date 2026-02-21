using Microsoft.AspNetCore.Mvc;
using MovieCatalogAPI2.Data;
using MovieCatalogAPI2.Models;
using MovieCatalogAPI2.Models.Requests;
using MovieCatalogAPI2.Services;
using System.Linq;

namespace MovieCatalogAPI2.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly AuthenticationService _auth;

        public UsersController(AppDbContext dbContext, AuthenticationService auth)
        {
            _db = dbContext;
            _auth = auth;
        }

        /// <summary>Register a new user</summary>
        [HttpPost("register")]
        public IActionResult CreateUser([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingUser = _db.Users.FirstOrDefault(u => u.Username == request.Username || u.Email == request.Email);
            if (existingUser != null) return Conflict("Username or email already exists.");

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _auth.HashPassword(request.Password),
                Role = "User"
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            return Ok(new { user.Id, user.Username, user.Email });
        }

        /// <summary>Login and receive JWT token</summary>
        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingUser = _db.Users.FirstOrDefault(u => u.Email == request.Email || u.Username == request.Username);
            if (existingUser == null) return Unauthorized("Incorrect credentials.");

            var verified = _auth.VerifyPassword(existingUser.PasswordHash, request.Password);
            if (!verified) return Unauthorized("Incorrect credentials.");

            var token = _auth.CreateJwtToken(existingUser);
            return Ok(new { token });
        }
    }
}
