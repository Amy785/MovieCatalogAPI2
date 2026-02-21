using Microsoft.AspNetCore.Mvc;
using MovieCatalogAPI2.Data;
using MovieCatalogAPI2.Models;
using System.Linq;

namespace MovieCatalogAPI.Controllers;

[ApiController]
[Route("api/genres")]
public class GenresController : ControllerBase
{
    private readonly AppDbContext _context;

    public GenresController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/genres
    [HttpGet]
    public IActionResult Get()
    {
        var genres = _context.Genres.ToList();
        return Ok(genres);
    }

    // POST /api/genres
    [HttpPost]
    public IActionResult Create(Genre genre)
    {
        _context.Genres.Add(genre);
        _context.SaveChanges();
        return Ok(genre);
    }
}