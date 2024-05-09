using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;

namespace MoviesAPI;

[ApiController]
[Route("api/movies/{movieId}/reviews")]
public class MovieReviewsController : ControllerBase
{
  private IMapper _mapper;
  private IMovieRepository _movieRepository;

  public MovieReviewsController(IMapper mapper, IMovieRepository movieRepository)
  {
    _movieRepository = movieRepository;
    _mapper = mapper;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<MovieReviewDto>>> GetReviewsByMovieId(int movieId)
  {
    var movieReviews = await _movieRepository.GetReviewsByMovieId(movieId);
    return Ok(_mapper.Map<IEnumerable<MovieReviewDto>>(movieReviews));
  }

  [HttpGet("{movieReviewId}")]
  public async Task<ActionResult<MovieReviewDto>> GetMovieReviewById(int movieId, int movieReviewId)
  {
    if (!await _movieRepository.MovieExists(movieId))
    {
      return NotFound($"Movie with id {movieId} doesnt exist");
    }

    if (!await _movieRepository.MovieReviewExist(movieReviewId))
    {
      return NotFound($"Movie Review with id {movieReviewId} doesnt exist");
    }

    var movieReviewEntity = await _movieRepository.GetMovieReviewById(movieId, movieReviewId);
    return Ok(_mapper.Map<MovieReviewDto>(movieReviewEntity));
  }

  [HttpPost]
  public async Task<ActionResult> CreateReviewForMovieId(int movieId, [FromBody] MovieReviewForCreationDto MovieReview)
  {
    if (!await _movieRepository.MovieExists(movieId))
    {
      return NotFound($"Movie with id {movieId} doesnt exist");
    }

    var MovieReviewEntity = _mapper.Map<MovieReview>(MovieReview);
    await _movieRepository.AddReviewToMovieId(movieId, MovieReviewEntity);
    await _movieRepository.SaveChangesAsync();
    var newMovieReview = _mapper.Map<MovieReviewDto>(MovieReviewEntity);
    return Created($"movies/{movieId}/reviews/{newMovieReview.Id}", newMovieReview);
  }

  [HttpDelete("{movieReviewId}")]
  public async Task<ActionResult> DeleteReviewFromMovieId(int movieId, int movieReviewId)
  {
    if (!await _movieRepository.MovieExists(movieId))
    {
      return NotFound($"Movie with id {movieId} doesnt exist");
    }

    if (!await _movieRepository.MovieReviewExist(movieReviewId))
    {
      return NotFound($"Movie Review with id {movieReviewId} doesnt exist");
    }

    await _movieRepository.RemoveReviewFromMovieId(movieId, movieReviewId);
    await _movieRepository.SaveChangesAsync();
    return NoContent();
  }
}
