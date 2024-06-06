using AutoMapper;
using AutoMapper.Configuration.Conventions;
using MoviesAPI.Services;

namespace MoviesAPI;

public class MovieService
{
  private IMovieRepository _movieRepository;
  private IMapper _mapper;
  private PrincessTheatreService _princessTheatreService;

  public MovieService(IMovieRepository movieRepository, IMapper mapper, PrincessTheatreService princessTheatreService)
  {
    _movieRepository = movieRepository;
    _mapper = mapper;
    _princessTheatreService = princessTheatreService;
  }

  public async Task<IEnumerable<MovieWithoutCinemasDto>> GetMovies()
  {
    var moviesEntities = await _movieRepository.GetMoviesAsync();
    return _mapper.Map<IEnumerable<MovieWithoutCinemasDto>>(moviesEntities);
  }

  public async Task<MovieDto> GetMovieById(int movieId)
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

    return mappedMovie;
  }
}
