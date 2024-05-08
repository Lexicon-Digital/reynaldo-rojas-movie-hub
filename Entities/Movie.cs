using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MoviesAPI.Entities;

public class Movie
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  [Column("id")]
  public int Id { get; set; }

  [Required]
  [MaxLength(128)]
  [Column("title")]
  public string Title { get; set; }

  [Required]
  [Column("releaseDate")]
  public DateOnly ReleaseDate { get; set; }

  [Required]
  [MaxLength(64)]
  [Column("genre")]
  public string Genre { get; set; }

  [Required]
  [Column("runtime")]
  public int Runtime { get; set; }

  [Required]
  [Column("synopsis")]
  public string Synopsis { get; set; }

  [Required]
  [MaxLength(64)]
  [Column("director")]
  public string Director { get; set; }

  [Required]
  [MaxLength(8)]
  [Column("rating")]
  public string Rating { get; set; }

  [Required]
  [MaxLength(16)]
  [Column("princessTheatreMovieId")]
  public string PrincessTheatreMovieId { get; set; }

  public ICollection<MovieCinema> MovieCinema { get; set; } = new List<MovieCinema>();

  public Movie(string title, string genre, string synopsis, string director, string rating, string princessTheatreMovieId)
  {
    Title = title;
    Genre = genre;
    Synopsis = synopsis;
    Director = director;
    Rating = rating;
    PrincessTheatreMovieId = princessTheatreMovieId;
  }
}
