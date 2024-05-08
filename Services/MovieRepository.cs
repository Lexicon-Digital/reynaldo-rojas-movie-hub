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

  public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesASync(string? name, string? searchQuery, int pageNumber, int pageSize)
  {
    var collection = _context.Cities as IQueryable<City>;

    if (!string.IsNullOrWhiteSpace(name))
    {
      collection = collection.Where(c => c.Name == name);
    }

    if (!string.IsNullOrWhiteSpace(searchQuery))
    {
      collection = collection.Where(c => c.Name.Contains(searchQuery) || c.Description.Contains(searchQuery));
    }

    var totalItemCount = await collection.CountAsync();

    var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

    var collectionToReturn = await collection
      .OrderBy(c => c.Name)
      .Skip(pageSize * (pageNumber - 1))
      .Take(pageSize)
      .ToListAsync();

    return (collectionToReturn, paginationMetadata);
  }


  public async Task<IEnumerable<City>> GetCitiesASync()
  {
    return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
  }

  public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
  {
    if (includePointsOfInterest)
    {
      return await _context.Cities
                    .Include(c => c.PointsOfInterest)
                    .Where(c => c.Id == cityId)
                    .FirstOrDefaultAsync();
    }

    return await _context.Cities.
                  Where(c => c.Id == cityId)
                  .FirstOrDefaultAsync();
  }

  public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestASync(int cityId)
  {
    return await _context.PointsOfInterest
                  .Where(p => p.CityId == cityId)
                  .ToListAsync();
  }

  public async Task<PointOfInterest?> GetPointOfInterestASync(int cityId, int pointOfInterestId)
  {
    return await _context.PointsOfInterest
                  .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
                  .FirstOrDefaultAsync();
  }

  public async Task<bool> CityExistsAsync(int cityId)
  {
    return await _context.Cities.AnyAsync(c => c.Id == cityId);
  }

  public async Task AddPointOfInterestToCity(int cityId, PointOfInterest pointOfInterest)
  {
    var city = await GetCityAsync(cityId, false);
    if (city != null)
    {
      city.PointsOfInterest.Add(pointOfInterest);
    }
  }

  public void DeletePointOfInterest(PointOfInterest pointOfInterest)
  {
    _context.PointsOfInterest.Remove(pointOfInterest);
  }

  public async Task<bool> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync() >= 0;
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
}
