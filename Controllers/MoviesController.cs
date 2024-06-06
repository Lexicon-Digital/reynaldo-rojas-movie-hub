using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;

namespace MoviesAPI;

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
  private MovieService _movieService;

  public MoviesController(MovieService movieService)
  {
    _movieService = movieService;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<MovieDto>>> GetListOfMovies()
  {
    var movies = await _movieService.GetMovies();
    return Ok(movies);
  }

  [HttpGet("{movieId}")]
  public async Task<ActionResult> GetMovieById(int movieId)
  {
    var movie = await _movieService.GetMovieById(movieId);
    return Ok(movie);
  }
}
