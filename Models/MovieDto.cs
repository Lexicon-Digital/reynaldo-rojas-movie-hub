using MoviesAPI.Entities;

namespace MoviesAPI;

public class MovieDto : MovieWithoutCinemasDto
{
  public ICollection<CinemaDto> Cinemas { get; set; } = new List<CinemaDto>();
}
