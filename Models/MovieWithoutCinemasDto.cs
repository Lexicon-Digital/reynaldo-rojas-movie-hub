using System.Text.Json.Serialization;

namespace MoviesAPI;

public class MovieWithoutCinemasDto
{

  [JsonPropertyOrder(1)]
  public int Id { get; set; }

  [JsonPropertyOrder(2)]
  public string Title { get; set; } = String.Empty;

  [JsonPropertyOrder(3)]
  public DateOnly ReleaseDate { get; set; }

  [JsonPropertyOrder(4)]
  public string Genre { get; set; } = String.Empty;


  [JsonPropertyOrder(5)]
  public int Runtime { get; set; }

  [JsonPropertyOrder(6)]
  public string Synopsis { get; set; } = String.Empty;

  [JsonPropertyOrder(7)]
  public string Director { get; set; } = String.Empty;

  [JsonPropertyOrder(8)]
  public string Rating { get; set; } = String.Empty;
}
