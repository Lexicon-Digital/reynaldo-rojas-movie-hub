using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;

namespace MoviesAPI;

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
  private IMovieRepository _movieRepository;
  private IMapper _mapper;

  private PrincessTheatreService _princessTheatreService;

  public MoviesController(IMovieRepository movieRepository, IMapper mapper, PrincessTheatreService princessTheatreService)
  {
    _movieRepository = movieRepository;
    _mapper = mapper;
    _princessTheatreService = princessTheatreService;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<MovieDto>>> GetListOfMovies()
  {
    var moviesEntities = await _movieRepository.GetMoviesAsync();
    return Ok(_mapper.Map<IEnumerable<MovieWithoutCinemasDto>>(moviesEntities));
  }

  [HttpGet("{movieId}")]
  public async Task<ActionResult> GetMovieById(int movieId)
  {
    // Get movies
    var movieEntity = await _movieRepository.GetMovieWithCinemas(movieId);
    var mappedMovie = _mapper.Map<MovieDto>(movieEntity);

    // Get princess theatre cinemas using reference id
    List<CinemaDto> princessTheatreCinemas = await _princessTheatreService.GetCinemasByReferenceId(movieEntity?.PrincessTheatreMovieId.ToString());

    // Add princess theatre cinemas to existing cinemas
    foreach (CinemaDto princessTheatreCinema in princessTheatreCinemas)
    {
      mappedMovie.Cinemas.Add(princessTheatreCinema);
    }

    return Ok(mappedMovie);
  }
}
