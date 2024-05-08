using Microsoft.EntityFrameworkCore;
using MoviesAPI.Entities;

namespace MoviesAPI.DbContexts;

public class MovieAPIContext : DbContext
{

  public MovieAPIContext(DbContextOptions<MovieAPIContext> options) : base(options)
  {
  }

  public DbSet<City> Cities { get; set; }

  public DbSet<PointOfInterest> PointsOfInterest { get; set; }

  public DbSet<Movie> Movie { get; set; }

  public DbSet<Cinema> Cinema { get; set; }

  public DbSet<MovieCinema> MovieCinema { get; set; }

}
