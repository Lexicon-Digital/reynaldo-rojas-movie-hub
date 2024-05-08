using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesAPI.Entities;

public class MovieCinema
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  [Column("id")]
  public int Id { get; set; }

  [ForeignKey("movieId")]
  public Movie? Movie { get; set; }

  [ForeignKey("cinemaId")]
  public Cinema? Cinema { get; set; }

  [Required]
  [Column("showtime")]
  public DateOnly Showtime { get; set; }

  [Required]
  [Column("ticketPrice")]
  public decimal TicketPrice { get; set; }
}
