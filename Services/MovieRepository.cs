using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DbContexts;
using MoviesAPI.Entities;

namespace MoviesAPI.Services;

public class MovieRepository : IMovieRepository
{
  private MovieAPIContext _context;

  public MovieRepository(MovieAPIContext context)
  {
    _context = context ?? throw new ArgumentNullException(nameof(context));
  }

  public async Task<IEnumerable<Movie>> GetMoviesAsync()
  {
    return await _context.Movie.ToListAsync();
  }

  public async Task<Movie?> GetMovieWithCinemas(int movieId)
  {
    return await _context.Movie
        .Include(m => m.MovieCinema)
        .ThenInclude(mc => mc.Cinema)
        .Where(m => m.Id == movieId)
        .FirstOrDefaultAsync();
  }

  public async Task<Movie?> GetMovieById(int movieId)
  {
    return await _context.Movie
        .Where(m => m.Id == movieId)
        .FirstOrDefaultAsync();
  }

  public async Task<IEnumerable<MovieReview>> GetReviewsByMovieId(int movieId)
  {
    return await _context.MovieReview
        .Where(mr => mr.Movie.Id == movieId)
        .ToListAsync();
  }

  public async Task<bool> MovieExists(int movieId)
  {
    return await _context.Movie.AnyAsync(m => m.Id == movieId);
  }

  public async Task AddReviewToMovieId(int movieId, MovieReview movieReview)
  {
    var movie = await GetMovieById(movieId);
    if (movie != null)
    {
      movie.MovieReviews.Add(movieReview);
    }
  }

  public async Task<MovieReview?> GetMovieReviewById(int movieId, int movieReviewId)
  {
    return await _context.MovieReview
      .Where(mr => mr.Movie.Id == movieId && mr.Id == movieReviewId)
      .FirstOrDefaultAsync();
  }

  public async Task RemoveReviewFromMovieId(int movieId, int movieReviewId)
  {
    var movie = await GetMovieById(movieId);
    var movieReview = await GetMovieReviewById(movieId, movieReviewId);
    if (movie != null && movieReview != null)
    {
      movie.MovieReviews.Remove(movieReview);
    }
  }

  public async Task<bool> MovieReviewExist(int movieReviewId)
  {
    return await _context.MovieReview.AnyAsync(mr => mr.Id == movieReviewId);
  }
  public async Task<bool> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync() >= 0;
  }
}
