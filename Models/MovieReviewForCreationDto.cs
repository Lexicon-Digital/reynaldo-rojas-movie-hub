using System.ComponentModel.DataAnnotations;

namespace MoviesAPI;

public class MovieReviewForCreationDto
{
  [Required]
  public decimal Score { get; set; }

  [Required]
  public string Comment { get; set; } = String.Empty;

  public DateTime ReviewDate { get; set; }
}
