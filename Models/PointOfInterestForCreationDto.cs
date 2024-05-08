using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Models;

public class PointOfInterestForCreationDto
{

  [Required()]
  [MaxLength(50)]
  public string Name { get; set; } = String.Empty;

  [MaxLength(200)]
  public string? Description { get; set; }
}
