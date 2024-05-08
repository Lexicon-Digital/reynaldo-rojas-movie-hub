using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MoviesAPI.Entities;

public class Cinema
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  [Column("id")]
  public int Id { get; set; }

  [Required]
  [MaxLength(64)]
  [Column("name")]
  public string Name { get; set; }

  [Required]
  [Column("location")]
  public string Location { get; set; }

  public Cinema(string name, string location)
  {
    Name = name;
    Location = location;
  }
}
