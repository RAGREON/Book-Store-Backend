using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Store.Api.Models;

public class Book
{
  [Key]
  public int Id { get; set; }

  [Required]
  [StringLength(100)]
  public required string Name { get; set; }

  [Column(TypeName = "date")]
  public DateOnly ReleaseDate { get; set; }

  [StringLength(4000)]
  public string? Description { get; set; }

  public ICollection<Genre> Genres { get; set; } = new List<Genre>();
}