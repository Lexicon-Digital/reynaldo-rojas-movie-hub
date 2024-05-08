using MoviesAPI.Entities;

namespace MoviesAPI;

public class MovieDto
{
  public int Id { get; set; }
  public string Title { get; set; } = String.Empty;
  public DateOnly ReleaseDate { get; set; }
  public string Genre { get; set; } = String.Empty;
  public int Runtime { get; set; }
  public string Synopsis { get; set; } = String.Empty;
  public string Director { get; set; } = String.Empty;
  public string Rating { get; set; } = String.Empty;
  public ICollection<CinemaDto> Cinemas { get; set; } = new List<CinemaDto>();
}
