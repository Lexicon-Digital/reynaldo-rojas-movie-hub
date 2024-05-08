using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Entities;
using MoviesAPI.Services;

namespace MoviesAPI;

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
  private IMovieRepository _movieRepository;
  private IMapper _mapper;

  public MoviesController(IMovieRepository movieRepository, IMapper mapper)
  {
    _movieRepository = movieRepository;
    _mapper = mapper;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<MovieDto>>> GetListOfMovies()
  {
    var moviesEntities = await _movieRepository.GetMoviesAsync();
    return Ok(_mapper.Map<IEnumerable<MovieWithoutCinemasDto>>(moviesEntities));
  }

  [HttpGet("{movieId}")]
  public async Task<ActionResult<MovieDto>> GetMovieById(int movieId)
  {
    var movieEntity = await _movieRepository.GetMovieWithCinemas(movieId);
    return Ok(_mapper.Map<MovieDto>(movieEntity));
  }
}
