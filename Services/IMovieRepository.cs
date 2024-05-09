using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Entities;

namespace MoviesAPI.Services;

public interface IMovieRepository
{
  Task<IEnumerable<Movie>> GetMoviesAsync();

  Task<Movie?> GetMovieWithCinemas(int movieId);

  Task<IEnumerable<MovieReview>> GetReviewsByMovieId(int movieId);

  Task<bool> MovieExists(int movieId);

  Task<Movie?> GetMovieById(int movieId);

  Task AddReviewToMovieId(int movieId, MovieReview movieReview);

  Task<MovieReview?> GetMovieReviewById(int movieId, int movieReviewId);

  Task RemoveReviewFromMovieId(int movieId, int movieReviewId);

  Task<bool> MovieReviewExist(int movieReviewId);
  Task<bool> SaveChangesAsync();
}
