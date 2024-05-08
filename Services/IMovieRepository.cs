using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Entities;

namespace MoviesAPI.Services;

public interface IMovieRepository
{
  Task<IEnumerable<City>> GetCitiesASync();

  Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesASync(string? name, string? searchQuery, int pageNumber, int pageSize);

  Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);

  Task<IEnumerable<PointOfInterest>> GetPointsOfInterestASync(int cityId);

  Task<PointOfInterest?> GetPointOfInterestASync(int cityId, int pointOfInterestId);

  Task<bool> CityExistsAsync(int cityId);

  Task AddPointOfInterestToCity(int cityId, PointOfInterest pointOfInterest);

  void DeletePointOfInterest(PointOfInterest pointOfInterest);

  Task<bool> SaveChangesAsync();

  Task<IEnumerable<Movie>> GetMoviesAsync();

  Task<Movie?> GetMovieWithCinemas(int movieId);
}
