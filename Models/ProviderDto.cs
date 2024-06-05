using System.ComponentModel.DataAnnotations;

namespace MoviesAPI;

public class ProviderDto
{

  [Required]
  public string Provider { get; set; } = String.Empty;

  public IEnumerable<PrincessTheatreMovieDto> Movies { get; set; } = new List<PrincessTheatreMovieDto>();
}
