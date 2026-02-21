using Microsoft.AspNetCore.Mvc;

using MovieCatalogAPI2.Data;
using MovieCatalogAPI2.Models;
using System.Linq;

namespace MovieCatalogAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly AppDbContext _context;

    public MoviesController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/movies
    [HttpGet]
    public IActionResult Get()
    {
        var movies = _context.Movies.ToList();
        return Ok(movies);
    }

    // POST /api/movies
    [HttpPost]
    public IActionResult Create(Movie movie)
    {
        _context.Movies.Add(movie);
        _context.SaveChanges();
        return Ok(movie);
    }
}
