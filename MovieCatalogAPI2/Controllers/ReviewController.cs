using Microsoft.AspNetCore.Mvc;
using MovieCatalogAPI2.Data;
using MovieCatalogAPI2.Models;
using System.Linq;

namespace MovieCatalogAPI.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly AppDbContext _context;
    public ReviewsController(AppDbContext context)
    {
        _context = context;
    }

    // GET /api/reviews
    [HttpGet]
    public IActionResult Get()
    {
        var reviews = _context.Reviews.ToList();
        return Ok(reviews);
    }

    // POST /api/reviews
    [HttpPost]
    public IActionResult Create(Review review)
    {
        _context.Reviews.Add(review);
        _context.SaveChanges();
        return Ok(review);
    }
}