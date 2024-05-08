using System.Text.Json.Serialization;

namespace MoviesAPI;

public class MovieWithoutCinemasDto
{
  public int Id { get; set; }
  public string Title { get; set; } = String.Empty;
  public DateOnly ReleaseDate { get; set; }
  public string Genre { get; set; } = String.Empty;
  public int Runtime { get; set; }
  public string Synopsis { get; set; } = String.Empty;
  public string Director { get; set; } = String.Empty;
  public string Rating { get; set; } = String.Empty;
}
