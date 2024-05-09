using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MoviesAPI.Entities;

namespace MoviesAPI;

public class MovieReview
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  [Column("id")]
  public int Id { get; set; }

  [Required]
  [ForeignKey("movieId")]
  public Movie? Movie { get; set; }

  [Required]
  [Column("score")]
  public decimal Score { get; set; }

  [Required]
  [Column("comment")]
  public string Comment { get; set; } = String.Empty;

  [Required]
  [Column("reviewDate")]
  public DateTime ReviewDate { get; set; }
}
